using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public class Person
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Name { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(50)]
    public string? Email { get; set; }
}
