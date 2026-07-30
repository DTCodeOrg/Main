using Domain.Model;
using Main.Common;
using Main.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Main.Infrastructure.CrosscuttingHelperServices;

public class TokenService: ITokenService
{
    private readonly TokenValidationParameters _validationParameters;
    private readonly byte[] _signingKey;
    private readonly ITokenRepository _tokenRepository;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly ITenantUserRepository _tenantUserRepository;

    public TokenService (IConfiguration config,ITokenRepository tokenRepository,
        IApplicationUserRepository applicationUserRepository,
        ITenantUserRepository tenantUserRepository)
    {
        _tokenRepository = tokenRepository;
        _signingKey = Encoding.UTF8.GetBytes (config["Jwt:Key"]!);
        _applicationUserRepository = applicationUserRepository;
        _tenantUserRepository = tenantUserRepository;

        _validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey (_signingKey),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ClockSkew = TimeSpan.Zero
        };
    }

    public async Task<string> GenerateAccessToken (
    string userId,
    Guid tenantId,
    string formattedTenantRole,
    string userRole,
    string userName,
    string email,
    int expiryInMinutes)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        // 1. Maintain a clean payload claims array 
        var claims = new List<Claim>
    {
        new(ClaimTypes.NameIdentifier, userId),
        new(ClaimTypes.Role, "User"), // Global fallback role mapping
        new("TenantId", tenantId.ToString()),
        new("TenantRole", formattedTenantRole),
        new("UserRole", userRole),
        new("UserName", userName),
        new("Email", email)
    };

        // 2. FIX: Do NOT pass an authentication type string here for JWT generation
        var claimsIdentity = new ClaimsIdentity(claims);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = DateTime.UtcNow.AddMinutes(expiryInMinutes),
            SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(_signingKey),
            SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        // 3. REMOVED: SaveRefreshToken is completely deleted from here.
        // The encrypted string is written and passed back cleanly to your Sign-In flow.
        return tokenHandler.WriteToken (token);
    }

    public string GenerateRefreshToken () =>
        Convert.ToBase64String (RandomNumberGenerator.GetBytes (62));


    public async Task<bool> RevokeUserRefreshTokensAsync (string userId,Guid tenantId)
    {
        bool result = await _tokenRepository.LogoutRevokeUserRefreshTokensAsync(userId,tenantId);
        return result;
    }

    public async Task<bool> SaveRefreshToken (string userId,Guid tenantId,string token)
    {
        bool result = await _tokenRepository.SaveTokenAsync(userId,tenantId,token);
        return result;
    }

    public ClaimsPrincipal? ValidateAndDecryptToken (string token,out SecurityToken? validatedToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            return tokenHandler.ValidateToken (token,_validationParameters,out validatedToken);
        }
        catch
        {
            validatedToken = null;
            return null;
            // Return null if validation fails
        }
    }

    public async Task<TokenResult> RotateRefreshTokenAsync (string token,Guid tenantId,int accessExpiryMinutes,int refreshExpiryDays)
    {
        // 1. Fetch token record from DB/Redis by its raw token string and matching tenant context
        UserRefreshToken? storedTokenRecord = await _tokenRepository.GetRefreshTokens (token,tenantId);

        // 2. Fetch the actual User record from your data tier to ensure their account is still active
        var user = await _applicationUserRepository.ApplicationUsers (storedTokenRecord!.UserId);

        if ( user == null )
        {
            throw new SecurityException ("User account associated with this token is suspended.");
        }



        if ( storedTokenRecord == null || storedTokenRecord.IsRevoked )
        {

            _ = await _tokenRepository.RevokeAllUserTokensAsync (user.Id,tenantId);

            throw new SecurityException ("Invalid, expired, or revoked refresh token session.");
        }

        // 3. Fetch user roles dynamically from DB mapping
        var userRole = "User";

        var tenantRole = await _tenantUserRepository.GetByUserIdAsync(user.Id, tenantId);

        string formattedTenantRole = $"{user.Id}:{tenantId}:{tenantRole}";

        // 4. Generate clean tokens using real database state
        var newAccessToken = await GenerateAccessToken(user.Id, tenantId, formattedTenantRole, userRole, user.UserName!, user.Email!, accessExpiryMinutes);

        var newRefreshTokenStr = GenerateRefreshToken();

        // 5. Invalidate old token record (Rotate/Replace for security)
        storedTokenRecord.IsRevoked = true;

        _ = await _tokenRepository.UpdateTokenAsync (storedTokenRecord);

        // 6. Save the new replacement token down to database
        _ = await SaveRefreshToken (user.Id,tenantId,newRefreshTokenStr);

        return new TokenResult (true)
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshTokenStr
        };
    }
}