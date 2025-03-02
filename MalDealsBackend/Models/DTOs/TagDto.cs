using MalDealsBackend.Models.Entitys;

namespace MalDealsBackend.Models.DTOs
{
    public record TagDto(
        string Title
    )
    {
        public TagEntity ToModel() => new()
        {
            Title = Title,
        };
    }
}