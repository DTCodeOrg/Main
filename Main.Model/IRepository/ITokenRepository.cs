using Domain.Model;

namespace Main.IRepository;

public interface ITokenRepository
{
    Task<UserRefreshToken> GetRefreshTokens (string token,Guid tenantId);

    Task<bool> LogoutRevokeUserRefreshTokensAsync (string userId,Guid tenantId);

    Task<UserRefreshToken?> GetSavedRefreshTokenAsync (string userId,Guid tenantId);

    Task<bool> UpdateTokenAsync (UserRefreshToken userRefreshToken);

    Task<bool> RevokeAllUserTokensAsync (string userId,Guid tenantId);

    Task<bool> SaveTokenAsync (string userId,Guid tenantId,string token);

    Task<bool> RotateRefreshTokenAsync (UserRefreshToken savedRefreshToken,Task<string> newAccessToken,string newRefreshTokenString);
}