using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MalDealsBackend.Models.Entitys
{
    [Table("user_market_deals")]
    public class UserMarketDealEntity
    {
        [Key]
        [Column("user_id")]
        [Required]
        public required Guid UserId { get; set; }
        [Column("deal_id")]
        [Required]
        public required Guid DealId { get; set; }
    }
}