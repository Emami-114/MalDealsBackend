using MalDealsBackend.Models.Entitys;

namespace MalDealsBackend.Models.DTOs
{

    public record CategoryModelDto
    {
        public Guid Id {get; init;}
        public required string Title { get; init; }
        public string? Thumbnail { get; init; }
        public bool IsPublic { get; init; } = true;
        public string[]? SubCategoryIds { get; init; }
        public Guid? ParentCategoryId { get; init; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsMainCategory => ParentCategoryId == null;

        public static CategoryModelDto ToDto(CategoryEntity category) => new() {
            Id = category.Id,
            Title = category.Title,
            Thumbnail = category.Thumbnail,
            IsPublic = category.IsPublic,
            SubCategoryIds = category.SubCategoryIds,
            ParentCategoryId = category.ParentCategoryId,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt
        };
    }

    public record CreateCategoryModelDto
    {
        public required string Title { get; init; }
        public string? Thumbnail { get; init; }
        public bool IsPublic { get; init; } = true;
        public string[]? SubCategoryIds { get; init; }
        public Guid? ParentCategoryId { get; init; }
        public CategoryEntity ToCategoryEntity() => new()
        {
            Title = Title,
            Thumbnail = Thumbnail,
            IsPublic = IsPublic,
            SubCategoryIds = SubCategoryIds,
        };
    }

    public record UpdateCategoryModelDto
    (
         string Title,
         string? Thumbnail,
         bool IsPublic,
         string[]? SubCategoryIds
    ) {
         public CategoryEntity ToCategoryEntity(CategoryEntity category) => new()
        {
            Id = category.Id,
            Title = Title,
            Thumbnail = Thumbnail,
            IsPublic = IsPublic,
            SubCategoryIds = SubCategoryIds,
            CreatedAt = category.CreatedAt,
            UpdatedAt = DateTime.UtcNow
        };
    }

}