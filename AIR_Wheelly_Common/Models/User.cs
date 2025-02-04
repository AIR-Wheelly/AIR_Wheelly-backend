using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AIR_Wheelly_Common.Models;

public class User
{
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [JsonIgnore]
    public string Password { get; set; }

    [Required]
    [Column(TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public ICollection<CarReservation> CarReservations { get; set; } = new List<CarReservation>();
    [JsonIgnore]
    public ICollection<Review> Reviews { get; set; }
    public User()
    {
        CreatedAt = DateTime.Now;
    }
}