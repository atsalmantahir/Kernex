using System.ComponentModel.DataAnnotations;

namespace Application.Common.Models;

public class EntityExternalIdentifier
{
    [Required]
    public string ExternalIdentifier { get; set; }
}
