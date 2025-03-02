using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MalDealsBackend.Models.Entitys
{
    [Table("tags")]
    [Index(nameof(Title), IsUnique = true)]
    public class TagEntity
    {
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Column("title", TypeName = "VARCHAR(255)")]
        [Required]
        [MaxLength(255)]
        public required string Title { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}