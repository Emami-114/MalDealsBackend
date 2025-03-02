using MalDeals.Data;
using MalDeals.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace MalDeals.Services
{
    public class ProviderServices(AppDbContext dbContext)
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<IEnumerable<ProviderEntity>> GetProvidersAsync()
        {
            IEnumerable<ProviderEntity> providers = await _dbContext.Providers.AsNoTracking().ToListAsync();
            return providers;
        }

        public async Task<ProviderEntity?> GetProviderByIdAsync(Guid id)
        {
            ProviderEntity? provider = await _dbContext.Providers.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return provider;
        }

        public async Task<ProviderEntity> CreateProviderAsync(ProviderEntity providerModel)
        {
            ProviderEntity provider = providerModel;
            _dbContext.Providers.Add(provider);
            await _dbContext.SaveChangesAsync();
            return provider;
        }

        public async Task UpdateProviderAsync(ProviderEntity providerModel) 
        {
            ProviderEntity updateProvider = providerModel;
            _dbContext.Providers.Update(updateProvider);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProviderAsync(ProviderEntity provider)
        {
            _dbContext.Providers.Remove(provider);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> ProviderExistsByTitleAsync(string title)
        {
            return await _dbContext.Providers.AnyAsync(x => x.Title.ToLower() == title.ToLower());
        }
    }
}