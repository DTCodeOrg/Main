using Main.Common;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Main.Infrastructure.CrosscuttingHelperServices;

public interface ITokenService
{
    Task<string> GenerateAccessToken
    (string userId,Guid tenantId,string formatedTenantRole,string userRole,string userName,string email,int expiryInMinutes);

    string GenerateRefreshToken ();

    Task<bool> SaveRefreshToken (string userId,Guid tenantId,string token);

    ClaimsPrincipal? ValidateAndDecryptToken (string token,out SecurityToken? validatedToken);

    Task<bool> RevokeUserRefreshTokensAsync (string userId,Guid tenantId);

    Task<TokenResult> RotateRefreshTokenAsync (string token,Guid tenantId,string userId,int accessExpiryMinutes,int refreshExpiryDays);
}