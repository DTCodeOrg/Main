using Main.Model.Base;
using System.ComponentModel.DataAnnotations;

namespace Main.Model.Log;

public class ExceptionLogs: BaseEntity
{
    public ExceptionLogs ()
    {
    }

    [Key]
    public long Id
    {
        get; set;
    }

    [Required]
    [StringLength (255)]
    public string ExceptionType { get; set; } = string.Empty;


    [Required]
    public int StatusCode
    {
        get; set;
    }


    [Required]
    [StringLength (50)]
    public string ErrorCode { get; set; } = string.Empty;


    [Required]
    [StringLength (3000)]
    public string DetailedMessage { get; set; } = string.Empty;

    [StringLength (6000)]
    public string? StackTrace
    {
        get; set;
    }

    [StringLength (6000)]
    public string? InnerException
    {
        get; set;
    }


    [Required]
    [StringLength (500)]
    public string UserMessage { get; set; } = string.Empty;


    [StringLength (500)]
    public string? RequestUrl
    {
        get; set;
    }


    [StringLength (10)]
    public string? HttpMethod
    {
        get; set;
    }


    [StringLength (6000)]
    public string? RequestHeaders
    {
        get; set;
    }


    [StringLength (6000)]
    public string? RequestBody
    {
        get; set;
    }


    public string? UserId
    {
        get; set;
    }


    [StringLength (45)]
    public string? ClientIpAddress
    {
        get; set;
    }


    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    [StringLength (50)]
    public string? Source
    {
        get; set;
    }


    [StringLength (50)]
    public string? Environment
    {
        get; set;
    }


    [StringLength (6000)]
    public string? CustomData
    {
        get; set;
    }


    public bool IsResolved { get; set; } = false;


    [StringLength (6000)]
    public string? ResolutionNotes
    {
        get; set;
    }


    public DateTime? ResolvedAt
    {
        get; set;
    }


    [Required]
    public int OccurrenceCount { get; set; } = 1;
}
