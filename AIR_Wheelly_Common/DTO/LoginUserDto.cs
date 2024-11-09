using System.ComponentModel.DataAnnotations;

namespace AIR_Wheelly_Common.DTO;

public class LoginUserDto
{
    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MaxLength(100)]
    public string Password { get; set; }
}