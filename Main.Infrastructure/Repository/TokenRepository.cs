using Domain.Model;
using Main.Infrastructure.DatabaseContext;
using Main.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Main.Repository;

public class TokenRepository: ITokenRepository
{
    private readonly IdentityAppDbContext _context;

    public TokenRepository (IdentityAppDbContext context)
    {
        _context = context;
    }

    public async Task<UserRefreshToken> GetRefreshTokens (string token,Guid tenantId)
    {
        var savedRefreshToken = await _context.ApplicationUserRefreshTokens
        .FirstOrDefaultAsync(t => t.Token == token);

        if ( savedRefreshToken == null )
        {
            throw new UnauthorizedAccessException ("Invalid token.");
        }

        if ( savedRefreshToken.IsRevoked )
        {
            _ = await LogoutRevokeUserRefreshTokensAsync (savedRefreshToken.UserId,tenantId);

            throw new UnauthorizedAccessException ("Compromised token detected. All sessions revoked.");
        }

        return savedRefreshToken;
    }

    public async Task<bool> LogoutRevokeUserRefreshTokensAsync (string userId,Guid tenantId)
    {
        var activeTokens = await _context.ApplicationUserRefreshTokens
        .Where(t => t.UserId == userId && t.TenantId == tenantId && !t.IsRevoked)
        .ToListAsync();

        if ( activeTokens == null || !activeTokens.Any () )
        {
            return true;
        }

        foreach ( var token in activeTokens )
        {
            token.IsRevoked = true;
        }

        int result = await _context.SaveChangesAsync();

        return result > 0;
    }


    public async Task<UserRefreshToken?> GetSavedRefreshTokenAsync (string userId,Guid tenantId)
    {
        UserRefreshToken? userRefreshToken =
        await _context.ApplicationUserRefreshTokens.FirstOrDefaultAsync<UserRefreshToken>
        (a => a.UserId == userId && a.TenantId == tenantId);

        return userRefreshToken;
    }

    public async Task<bool> UpdateTokenAsync (UserRefreshToken userRefreshToken)
    {
        _ = _context.ApplicationUserRefreshTokens.Update (userRefreshToken);
        var result = await _context.SaveChangesAsync ();

        return result > 0;
    }

    public async Task<bool> RevokeAllUserTokensAsync (string userId,Guid tenantId)
    {
        var allUserTokens = await _context.ApplicationUserRefreshTokens
        .Where(t => t.UserId == userId && t.TenantId == tenantId && !t.IsRevoked)
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
        UserRefreshToken newRefreshToken = new ()
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = token,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            IsRevoked = false,
            ReplacedByToken = null,
            TenantId = tenantId
        };

        _ = _context.ApplicationUserRefreshTokens.Add (newRefreshToken);
        int  result = await _context.SaveChangesAsync ();

        return result > 0;
    }

    public async Task<bool> RotateRefreshTokenAsync (
        UserRefreshToken savedRefreshToken,
        string newAccessToken,
        string newRefreshTokenString)
    {
        savedRefreshToken.IsRevoked = true;
        savedRefreshToken.ReplacedByToken = newRefreshTokenString;
        savedRefreshToken.ModifiedDate = DateTime.UtcNow;

        // Insert the new child token into the chain
        var newRefreshTokenEntity = new UserRefreshToken
        {
            Id = Guid.NewGuid(),
            Token = newRefreshTokenString,
            UserId = savedRefreshToken.UserId,
            IsRevoked = false,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            TenantId = savedRefreshToken.TenantId ,
            ReplacedByToken = null
        };

        _ = _context.ApplicationUserRefreshTokens.Add (newRefreshTokenEntity);
        var result = await _context.SaveChangesAsync ();

        return result > 0;
    }
}
