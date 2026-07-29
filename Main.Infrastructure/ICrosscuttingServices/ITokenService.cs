using Main.Common;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Main.Infrastructure.CrosscuttingHelperServices;

public interface ITokenService
{
    Task<string> GenerateAccessToken
    (string userId,Guid tenantId,string formatedTenantRole,string userRole,string userName,string email,int expiryInMinutes,int days);

    string GenerateRefreshToken ();

    Task<bool> SaveRefreshToken (string userId,Guid tenantId,string token);

    ClaimsPrincipal? ValidateAndDecryptToken (string token,out SecurityToken? validatedToken);

    Task<bool> RevokeUserRefreshTokensAsync (string userId,Guid tenantId);

    Task<TokenResponseModel> RotateRefreshTokenAsync (string currentToken,Guid tenantId,string userId,string formatedTenantRole,string userRole,string userName,string email,int expiryInMinutes,int days);
}