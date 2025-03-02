using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MalDealsBackend.Models.Entitys
{
    [Table("categories")]
    [Index(nameof(Title), IsUnique = true)]
    public class CategoryEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Column("title", TypeName = "VARCHAR(255)")]
        [Required]
        [MinLength(3, ErrorMessage = "Title must be at least 3 characters long.")]
        public required string Title { get; set; }
        [Column("thumbnail")]
        public string? Thumbnail { get; set; }
        [Column("is_public")]
        public bool IsPublic { get; set; } = true;
        [Column("sub_category_ids")]
        public string[]? SubCategoryIds { get; set; }
        [Column("created_at", TypeName = "TIMESTAMP WITH TIME ZONE")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Column("updated_at", TypeName = "TIMESTAMP WITH TIME ZONE")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}