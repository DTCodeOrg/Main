using Main.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Main.Model.Identity;

public class TenantSmtpServer: RootBaseEntity
{
    public TenantSmtpServer ()
    {
    }

    [Key]
    public int SmtpServerId
    {
        get; set;
    }

    public string? SmtpHostServer { get; set; } = string.Empty;

    public int? Port { get; set; } = 587;

    public string? Username
    {
        get; set;
    }

    public string? Password
    {
        get; set;
    }

    public string? SmtpEmail
    {
        get; set;
    }

    public Guid TenantId
    {
        get; set;
    }

    [ForeignKey (nameof (TenantId))]
    public virtual Tenant? Tenant
    {
        get; set;
    }
}