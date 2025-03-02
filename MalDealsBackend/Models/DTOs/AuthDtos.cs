using System.ComponentModel.DataAnnotations;
using MalDeals.Models.Entitys;

namespace MalDeals.Models.DTOs
{
    public record RegisterUserModelDto(
        string Name,
        [EmailAddress(ErrorMessage = "Invalid email address")]
        string Email,
        [MinLength(6,ErrorMessage ="password must have at least 6 characters")]
        string Password
    )
    {
        public UserEntity ToModel() => new()
        {
            Name = Name,
            Email = Email,
            Password = Password
        };
    }

    public record LoginUserModelDto(
        string Email,
        string Password
    );

    public record UpdateUserVerifidDto(
        bool Verified
    )
    {
        public UserEntity ToModel(UserEntity user) => new()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
            Role = user.Role,
            Verified = Verified,
            CreatedAt = user.CreatedAt
        };
    }
}