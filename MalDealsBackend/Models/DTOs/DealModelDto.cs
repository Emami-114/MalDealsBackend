using MalDealsBackend.Models.Entitys;

namespace MalDealsBackend.Models.DTOs
{
    public record DealModelDto(
    Guid Id,
    string Title,
    bool IsFree,
    decimal? Price,
    decimal? OfferPrice,
    string? Provider,
    string? ProviderUrl,
    string? ThumbnailUrl,
    string? UserId,
    string? CouponCode,
    string? ExpirationDate,
    DateTime UpdatedAt
    ) 
    {
        public static IEnumerable<DealModelDto> ToDtos(IEnumerable<DealEntity> deals)
        {
            return deals.Select(deal => new DealModelDto(
                deal.Id,
                deal.Title,
                deal.IsFree,
                deal.Price,
                deal.OfferPrice,
                deal.Provider,
                deal.ProviderUrl,
                deal.ThumbnailUrl,
                deal.UserId,
                deal.CouponCode,
                deal.ExpirationDate,
                deal.UpdatedAt
            ));
        }
    }
    
    public record DealDetailModelDto(
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
        
        public static DealDetailModelDto ToDto(DealEntity deal)
        {
            return new DealDetailModelDto(
                deal.Id,
                deal.Title,
                deal.Description,
                deal.Categories,
                deal.IsFree,
                deal.Price,
                deal.OfferPrice,
                deal.Provider,
                deal.ProviderUrl,
                deal.ThumbnailUrl,
                deal.ImagesUrl,
                deal.UserId,
                deal.Tags,
                deal.ShipingCost,
                deal.VideoUrl,
                deal.CouponCode,
                deal.IsPublish,
                deal.ExpirationDate,
                deal.CreatedAt,
                deal.UpdatedAt
            );
        }
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