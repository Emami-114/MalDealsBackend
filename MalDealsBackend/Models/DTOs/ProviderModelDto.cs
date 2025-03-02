using MalDeals.Models.Entitys;

namespace MalDeals.Models.DTOs
{
  public record CreateProviderModelDto
  (
   string Title,
   string? LogoUrl,
   string? Url
 )
  {
    public ProviderEntity ToModel() => new()
    {
      Title = Title,
      LogoUrl = LogoUrl,
      Url = Url
    };
  }

  public record UpdateProviderModelDto(
    string Title,
    string? LogoUrl,
    string? Url
  )
  {
    public ProviderEntity ToModel(ProviderEntity provider) => new()
    {
      Id = provider.Id,
      Title = Title ?? provider.Title,
      LogoUrl = LogoUrl ?? provider.LogoUrl,
      Url = Url ?? provider.Url,
      CreatedAt = provider.CreatedAt,
      UpdatedAt = DateTime.UtcNow
    };
  }
}