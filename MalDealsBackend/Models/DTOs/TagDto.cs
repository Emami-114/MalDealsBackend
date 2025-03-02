using MalDeals.Models.Entitys;

namespace MalDeals.Models.DTOs
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