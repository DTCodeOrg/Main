using Domain.Model;
using Main.Common;
using Main.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
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
        _ = await _tokenRepository.SaveTokenAsync (userId,tenantId,token);
        return true;
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
        UserRefreshToken savedRefreshToken = await _tokenRepository.GetRefreshTokens  ( token, tenantId);

        // 2. Fetch the actual User record from your data tier to ensure their account is still active
        ApplicationUser? user = await _applicationUserRepository.ApplicationUsers (savedRefreshToken.UserId);


        // 2. Get tenant specific role (find for user)
        TenantUser? tenantUser = await _tenantUserRepository.GetByUserIdAsync(savedRefreshToken.UserId, tenantId);

        string tenantRole = tenantUser?.TenantRole!;

        string formatedTenantRole = $"{savedRefreshToken.UserId ?? ""}:{tenantId}:{tenantRole}";

        // 3. Generate new pair
        var newAccessToken = GenerateAccessToken(savedRefreshToken.UserId!, tenantId, formatedTenantRole, tenantRole, user?.UserName!, user?.Email!, accessExpiryMinutes);

        var newRefreshTokenString = GenerateRefreshToken();

        var result = await _tokenRepository.RotateRefreshTokenAsync (savedRefreshToken,newAccessToken,newRefreshTokenString);

        return new TokenResult (result)
        {
            AccessToken = newAccessToken?.ToString () ?? "",
            RefreshToken = newRefreshTokenString
        };
    }
}