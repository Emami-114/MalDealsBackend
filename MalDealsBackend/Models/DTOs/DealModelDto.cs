using MalDealsBackend.Models.Entitys;

namespace MalDealsBackend.Models.DTOs
{
    public record DealModelDto(
    Guid Id,
    string Title,
    string? Description,
    string[]? Categories,
    bool IsFree,
    decimal? Price,
    decimal? OfferPrice,
    string? Provider,
    string? ProviderUrl,
    string? ThumbnailUrl,
    string[]? ImagesUrl,
    string? UserId,
    string[]? Tags,
    decimal ShipingCost,
    string? VideoUrl,
    string? CouponCode,
    bool IsPublish,
    string? ExpirationDate,
    DateTime CreatedAt,
    DateTime UpdatedAt
    )
    {
        public DealEntity ToModel() => new()
        {
            Id = Id,
            Title = Title,
            Description = Description,
            Categories = Categories,
            IsFree = IsFree,
            Price = Price,
            OfferPrice = OfferPrice,
            Provider = Provider,
            ProviderUrl = ProviderUrl,
            ThumbnailUrl = ThumbnailUrl,
            ImagesUrl = ImagesUrl,
            UserId = UserId,
            Tags = Tags,
            ShipingCost = ShipingCost,
            VideoUrl = VideoUrl,
            CouponCode = CouponCode,
            IsPublish = IsPublish,
            ExpirationDate = ExpirationDate,
            CreatedAt = CreatedAt,
            UpdatedAt = UpdatedAt
        };
    }

    public record CreateDealDto
    (
    string Title,
    string? Description,
    string[]? Categories,
    bool IsFree,
    decimal? Price,
    decimal? OfferPrice,
    string? Provider,
    string? ProviderUrl,
    string? ThumbnailUrl,
    string[]? ImagesUrl,
    string? UserId,
    string[]? Tags,
    decimal ShipingCost,
    string? VideoUrl,
    string? CouponCode,
    bool IsPublish,
    string? ExpirationDate
    )
    {
        public DealEntity ToModel() => new()
        {
            Title = Title,
            Description = Description,
            Categories = Categories,
            IsFree = IsFree,
            Price = Price,
            OfferPrice = OfferPrice,
            Provider = Provider,
            ProviderUrl = ProviderUrl,
            ThumbnailUrl = ThumbnailUrl,
            ImagesUrl = ImagesUrl,
            UserId = UserId,
            Tags = Tags,
            ShipingCost = ShipingCost,
            VideoUrl = VideoUrl,
            CouponCode = CouponCode,
            IsPublish = IsPublish,
            ExpirationDate = ExpirationDate
        };
    };
    public record UpdateDealModelDto
    (
    string Title,
    string? Description,
    string[]? Categories,
    bool IsFree,
    decimal? Price,
    decimal? OfferPrice,
    string? Provider,
    string? ProviderUrl,
    string? ThumbnailUrl,
    string[]? ImagesUrl,
    string? UserId,
    string[]? Tags,
    decimal ShipingCost,
    string? VideoUrl,
    string? CouponCode,
    bool IsPublish,
    string? ExpirationDate
    )
    {
        public DealEntity ToModel(DealEntity deal) => new()
        {
            Id = deal.Id,
            Title = Title,
            Description = Description,
            Categories = Categories,
            IsFree = IsFree,
            Price = Price,
            OfferPrice = OfferPrice,
            Provider = Provider,
            ProviderUrl = ProviderUrl,
            ThumbnailUrl = ThumbnailUrl,
            ImagesUrl = ImagesUrl,
            UserId = UserId,
            Tags = Tags,
            ShipingCost = ShipingCost,
            VideoUrl = VideoUrl,
            CouponCode = CouponCode,
            IsPublish = IsPublish,
            ExpirationDate = ExpirationDate,
            CreatedAt = deal.CreatedAt,
            UpdatedAt = DateTime.UtcNow
        };
    }
}