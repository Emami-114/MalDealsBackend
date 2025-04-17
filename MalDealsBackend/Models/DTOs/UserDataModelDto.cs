using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MalDealsBackend.Models.Entitys;

namespace MalDealsBackend.Models.DTOs
{
    public record UserDataModelDto(
        Guid UserId,
        string Name,
        string Email,
        string? PhotoUrl,
        string Role,
        bool Verified,
        DateTime CreatedAt,
        DateTime UpdatedAt
    ){
        public static IEnumerable<UserDataModelDto> ToDtos(IEnumerable<UserDataEntity> users) {
            return users.Select(user => new UserDataModelDto(
                user.UserId,
                user.Name,
                user.Email,
                user.PhotoUrl,
                user.Role,
                user.Verified,
                user.CreatedAt,
                user.UpdatedAt
            ));
        }

          public static UserDataModelDto ToDto(UserDataEntity user){
               return new UserDataModelDto(
                user.UserId,
                user.Name,
                user.Email,
                user.PhotoUrl,
                user.Role,
                user.Verified,
                user.CreatedAt,
                user.UpdatedAt
               );
        }

    }

    public record UpdateUserData(
        string? Name,
        string? Email,
        string? PhotoUrl
    ) {
        public static UserDataEntity ToUserDataEntity(UserDataEntity user, UpdateUserData update) => new() {
            UserId = user.UserId,
            Name = update.Name ?? user.Name,
            Email = update.Email ?? user.Email,
            PhotoUrl = update.PhotoUrl ?? user.PhotoUrl,
            Role = user.Role,
            Verified = user.Verified,
            CreatedAt = user.CreatedAt,
            UpdatedAt = DateTime.UtcNow
        };
    }
}