using System.ComponentModel.DataAnnotations.Schema;


namespace MalDealsBackend.Models.Entitys;
[Table("swagger_users")]
public class SwaggerUserEntity
{
    [Column("id")]
    public int Id { get; set; }
    [Column("email")]
    public required string Email { get; set; }
    [Column("password")]
    public required string Password { get; set; }
}