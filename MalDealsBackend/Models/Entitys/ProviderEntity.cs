using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MalDealsBackend.Models.Entitys
{
    [Table("providers")]
    [Index(nameof(Title), IsUnique = true)]
    public class ProviderEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Column("title", TypeName = "VARCHAR(255)")]
        [Required]
        [MaxLength(255)]
        public required string Title { get; set; }
        [Column("logo_url")]
        public string? LogoUrl { get; set; }
        [Column("url")]
        public string? Url { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}