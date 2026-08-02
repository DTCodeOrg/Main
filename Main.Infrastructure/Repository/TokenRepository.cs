using Domain.Model;
using Main.Infrastructure.DatabaseContext;
using Main.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Main.Repository;

public class TokenRepository: ITokenRepository
{
    private readonly ApplicationDbContext _context;

    public TokenRepository (ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserRefreshToken> GetRefreshTokens (string token,Guid tenantId)
    {
        // 1. Find the token in the database
        var savedRefreshToken = await _context.UserRefreshTokens
        .FirstOrDefaultAsync(t => t.Token == token);

        if ( savedRefreshToken == null )
        {
            throw new UnauthorizedAccessException ("Invalid token.");
        }

        // 2. TOKEN CHAIN PROTECTION: Detection of Replay Attack
        if ( savedRefreshToken.IsRevoked )
        {
            // Malicious actor or leaked token! Revoke all tokens descending from or belonging to this user
            _ = await LogoutRevokeUserRefreshTokensAsync (savedRefreshToken.UserId,tenantId);

            throw new UnauthorizedAccessException ("Compromised token detected. All sessions revoked.");
        }

        return savedRefreshToken;
    }


    public async Task<bool> LogoutRevokeUserRefreshTokensAsync (string userId,Guid tenantId)
    {
        // 1. Fetch only the tokens that aren't already revoked to save processing power
        var activeTokens = await _context.UserRefreshTokens
        .Where(t => t.UserId == userId && t.MyTenantId == tenantId && !t.IsRevoked)
        .ToListAsync();

        // 2. Return true early if there is nothing to update anyway
        if ( activeTokens == null || !activeTokens.Any () )
        {
            return true;
        }

        // 3. Mutate the tracked entities directly
        foreach ( var token in activeTokens )
        {
            token.IsRevoked = true;
            // REMOVED: _context.UserRefreshTokens.Update(token);
            // EF Core automatically tracks this mutation because the entity was loaded via _context
        }

        // 4. Commit changes safely
        int result = await _context.SaveChangesAsync();

        return result > 0;
    }


    public async Task<UserRefreshToken?> GetSavedRefreshTokenAsync (string userId,Guid tenantId)
    {
        UserRefreshToken? userRefreshToken =
        await _context.UserRefreshTokens.FirstOrDefaultAsync<UserRefreshToken>
        (a => a.UserId == userId && a.MyTenantId == tenantId);

        return userRefreshToken;
    }

    public async Task<bool> UpdateTokenAsync (UserRefreshToken userRefreshToken)
    {
        _ = _context.UserRefreshTokens.Update (userRefreshToken);
        var result = await _context.SaveChangesAsync ();

        return result > 0;
    }

    public async Task<bool> RevokeAllUserTokensAsync (string userId,Guid tenantId)
    {
        var allUserTokens = await _context.UserRefreshTokens
        .Where(t => t.UserId == userId && t.MyTenantId == tenantId && !t.IsRevoked)
        .ToListAsync();

        foreach ( var token in allUserTokens )
        {
            token.IsRevoked = true;
        }

        int result = await _context.SaveChangesAsync ();

        return result > 0;
    }

    public async Task<bool> SaveTokenAsync (string userId,Guid tenantId,string token)
    {
        var savedRefreshToken = await _context.UserRefreshTokens
        .FirstOrDefaultAsync(t => t.MyTenantId == tenantId && t.UserId == userId && !t.IsRevoked);

        if ( savedRefreshToken != null )
        {
            savedRefreshToken.IsRevoked = true;
        }

        UserRefreshToken newRefreshToken = new ()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = token,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7), // Rolling expiration
            IsRevoked = false,
            MyTenantId = tenantId
        };

        _ = _context.UserRefreshTokens.Add (newRefreshToken);
        int  result = await _context.SaveChangesAsync ();

        return result > 0;
    }

    public async Task<bool> RotateRefreshTokenAsync (UserRefreshToken savedRefreshToken,Task<string> newAccessToken,string newRefreshTokenString)
    {
        // 4. Update the old token in the chain
        savedRefreshToken.IsRevoked = true;
        savedRefreshToken.ReplacedByToken = newRefreshTokenString;
        savedRefreshToken.ModifiedDate = DateTime.UtcNow;

        // 5. Insert the new child token into the chain
        var newRefreshTokenEntity = new UserRefreshToken
        {
            Id = Guid.NewGuid(),
            Token = newRefreshTokenString,
            UserId = savedRefreshToken.UserId,
            IsRevoked = false,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            MyTenantId = savedRefreshToken.MyTenantId
        };

        _ = _context.UserRefreshTokens.Add (newRefreshTokenEntity);
        var result = await _context.SaveChangesAsync (); // Saves both the update and the insertion safely


        return result > 0;
    }
}
