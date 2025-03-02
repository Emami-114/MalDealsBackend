using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MalDeals.Models;
using MalDeals.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MalDeals.Models.Entitys
{
    [Table("users")]
    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class UserEntity
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Column("name", TypeName = "varchar(255)")]
        [MaxLength(255)]
        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }
        [Column("email", TypeName = "varchar(255)")]
        [MaxLength(255)]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public required string Email { get; set; }
        [Column("password")]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "password must have at least 6 characters")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        [Column("role")]
        public string Role { get; init; } = UserRole.Customer.ToString();
        [Column("verified")]
        public bool Verified { get; set; } = false;

        [Column("created_at")]
        public DateTime CreatedAt = DateTime.UtcNow;
    }

    public enum UserRole
    {
        Admin,
        Creator,
        Customer
    }
    public static class UserRoleExtensions
    {
        public static UserRole ToUserRole(this string role)
        {
            if (Enum.TryParse(role, true, out UserRole result))
            {
                return result;
            }
            return UserRole.Customer;
        }
    }
}

