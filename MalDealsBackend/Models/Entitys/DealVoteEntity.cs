using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MalDealsBackend.Models.Entitys
{
    [Table("deal_vote")]
    public class DealVoteEntity
    {
        [Key]
        [Column("deal_id")]
        public Guid DealId { get; set; }
        [Column("user_id")]
        public Guid UserId { get; set; }
        [Column("value")]
        public int Value { get; set; } // 1 = Upvote, -1 = Downvote
    }
}