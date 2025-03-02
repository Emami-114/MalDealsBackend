using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MalDeals.Models.Entitys;
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