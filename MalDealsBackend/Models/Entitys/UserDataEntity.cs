using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MalDealsBackend.Models.Entitys
{
    [Table("user_data")]
    public class UserDataEntity
    {
        [Key]
        [ForeignKey("UserEntity")]
        [Column("user_id")]
        public Guid UserId { get; set; }
        [Column("name", TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        [Required(ErrorMessage = "Name is Required")]
        public required string Name { get; set; }
        [Column("email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public required string Email { get; set; }

        [Column("photo_url")]
        public string? PhotoUrl { get; set; }

         [Column("role")]
        public string Role { get; init; } = UserRole.Customer.ToString();
        [Column("verified")]
        public bool Verified { get; set; } = false;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}