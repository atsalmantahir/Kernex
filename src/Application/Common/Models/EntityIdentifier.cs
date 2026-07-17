using System.ComponentModel.DataAnnotations;

namespace Application.Common.Models;

public class EntityIdentifier
{
    [Required]
    public int Id { get; set; }
}
