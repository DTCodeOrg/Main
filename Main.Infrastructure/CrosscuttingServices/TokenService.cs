using Domain.Model;
using Main.Common;
using Main.Infrastructure.ICrosscuttingServices;
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
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            RoleClaimType = "UserRole",
            NameClaimType = "UserName"
        };
    }

    public async Task<string> GenerateAccessToken (
        string userId,Guid tenantId,string formattedTenantRole,string userRole,string userName,string email,int expiryInMinutes)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userId),
            new(ClaimTypes.Role, "User"),
            new("TenantId", tenantId.ToString()),
            new("TenantRole", formattedTenantRole),
            new("UserRole", userRole),
            new("UserName", userName),
            new("Email", email)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expiryInMinutes),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(_signingKey),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken (token);
    }

    public string GenerateRefreshToken () =>
        Convert.ToBase64String (RandomNumberGenerator.GetBytes (62));

    public async Task<bool> RevokeUserRefreshTokensAsync (string userId,Guid tenantId) =>
        await _tokenRepository.LogoutRevokeUserRefreshTokensAsync (userId,tenantId);

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
        }
    }

    public async Task<TokenResult> RotateRefreshTokenAsync (string token,Guid tenantId,int accessExpiryMinutes,int refreshExpiryDays)
    {
        UserRefreshToken savedRefreshToken = await _tokenRepository.GetRefreshTokens(token, tenantId);
        if ( savedRefreshToken == null )
        {
            return null!;
        }

        ApplicationUser? user = await _applicationUserRepository.ApplicationUsers(savedRefreshToken.UserId!);
        TenantUser? tenantUser = await _tenantUserRepository.GetByUserIdAsync(savedRefreshToken.UserId!, tenantId);

        string tenantRole = tenantUser?.TenantRole!;
        string formatedTenantRole = $"{savedRefreshToken.UserId}:{tenantId}:{tenantRole}";

        // FIXED: Added missing await keyword to pull raw string response instead of a Task object instance
        var newAccessToken = await GenerateAccessToken(savedRefreshToken.UserId!, tenantId, formatedTenantRole, tenantRole, user?.UserName!, user?.Email!, accessExpiryMinutes);
        var newRefreshTokenString = GenerateRefreshToken();

        var result = await _tokenRepository.RotateRefreshTokenAsync(savedRefreshToken, newAccessToken.ToString(), newRefreshTokenString);

        return new TokenResult (result)
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshTokenString
        };
    }
}
