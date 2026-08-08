using Main.Model.DomainModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model;

public class EmailOutboxMessage: RootBaseEntity
{
    public EmailOutboxMessage ()
    {
    }

    [Key]
    public int Id
    {
        get; set;
    }

    public string? ReceiverEmail
    {
        get; set;
    }

    public string? Subject
    {
        get; set;
    }

    public string? Body
    {
        get; set;
    }

    public DateTime? CreatedOnUtc
    {
        get; set;
    }

    public DateTime? ProcessedOnUtc
    {
        get; set;
    }

    public string? Error
    {
        get; set;
    }

    public int? RetryCount
    {
        get; set;
    }

    public Guid TenantId
    {
        get; set;
    }

    [ForeignKey ("TenantId")]
    public Tenant? Tenant
    {
        get; set;
    }
}
