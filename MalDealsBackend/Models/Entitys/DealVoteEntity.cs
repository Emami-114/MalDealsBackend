using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MalDealsBackend.Models.Entitys
{
    [Table("deal_vote")]
    public class DealVoteEntity
    {
        [Key]
        public Guid Id {get; set;} = Guid.NewGuid();
        [Column("deal_id")]
        public Guid DealId { get; set; }
        [Column("user_id")]
        public Guid UserId { get; set; }
    }
}