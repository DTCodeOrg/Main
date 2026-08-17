using Main.Common;
using Main.Common.Models;

namespace Main.Infrastructure.ICrosscuttingServices;

public interface IEmailSenderService
{
    Task<string> SendEmailAsync (string userId);

    Task SendEmailAsync (string email,string subject,string htmlMessage);

    Task SendEmailVerificationAsync (VerifyDataModel verifyEmailDataModel);

    Task SendResetPasswordEmailAsync (ResetDataModel resetEmailDataModel);

    Task SendEmailAsync (string to,string subject,string body,CancellationToken ct = default);
}
