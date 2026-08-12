using Main.Common;
using Main.Model.Base;
using System.ComponentModel.DataAnnotations;

namespace Main.Model.Tenant;

public class AllowedValue: BaseEntity
{
    public AllowedValue ()
    {
    }

    public AllowedValue (Country country,string text,EnumTenantVariable variable)
    {
        if ( string.IsNullOrEmpty (text) )
        {
            throw new ArgumentException ("Text not provided.");
        }

        Text = text;
        Variable = variable;
    }

    [Key]
    public long ValueID
    {
        get; set;
    }


    [Required]
    public string Text
    {
        get; set;
    }


    [Required]
    public EnumTenantVariable Variable
    {
        get; set;
    }


    public long ParentValueId
    {
        get; set;
    }
}
