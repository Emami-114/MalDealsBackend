using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MalDealsBackend.Models.Entitys
{
    [Table("deals")]
    [Index(nameof(Title), IsUnique = true)]
    public class DealEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Column("title", TypeName = "varchar(255)")]
        [MaxLength(255)]
        [Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("categories")]
        public string[]? Categories { get; set; }
        [Column("is_free")]
        public bool IsFree { get; set; } = false;
        [Column("price")]
        public decimal? Price { get; set; }
        [Column("offer_price")]
        public decimal? OfferPrice { get; set; }

        [Column("provider", TypeName = "VARCHAR(100)")]
        [MaxLength(100)]
        public string? Provider { get; set; }

        [Column("provider_url", TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        public string? ProviderUrl { get; set; }

        [Column("thumbnail_url", TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        public string? ThumbnailUrl { get; set; }
        [Column("images_url")]
        public string[]? ImagesUrl { get; set; }

        [Column("user_id", TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        public string? UserId { get; set; }

        [Column("tags")]
        public string[]? Tags { get; set; }

        [Column("shipping_cost")]
        public decimal ShipingCost { get; set; } = 0;

        [Column("video_url", TypeName = "VARCHAR(255)")]
        [MaxLength(255)]
        public string? VideoUrl { get; set; }

        [Column("coupon_code", TypeName = "VARCHAR(50)")]
        [MaxLength(50)]
        public string? CouponCode { get; set; }

        [Column("is_publish")]
        public bool IsPublish { get; set; } = false;

        [Column("expiration_date")]
        [MaxLength(50)]
        public string? ExpirationDate { get; set; }

        [Column("created_at", TypeName = "TIMESTAMP WITH TIME ZONE")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at", TypeName = "TIMESTAMP WITH TIME ZONE")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}