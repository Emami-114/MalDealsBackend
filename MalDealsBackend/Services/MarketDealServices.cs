using MalDeals.Data;
using MalDeals.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace MalDeals.Services
{
    public class MarketDealServices(AppDbContext dbContext)
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<IEnumerable<UserMarketDealEntity>> GetMarketDealByUserIdAsync(Guid id)
        {
            IEnumerable<UserMarketDealEntity> marketDeals = await _dbContext.UserMarketDeals.Where(x => x.UserId == id).AsNoTracking().ToListAsync();
            return marketDeals;
        }

        public async Task<UserMarketDealEntity> CreateUserMarketAsync(UserMarketDealEntity userMarket)
        {
            UserMarketDealEntity marketDeal = userMarket;
            _dbContext.UserMarketDeals.Add(marketDeal);
            await _dbContext.SaveChangesAsync();
            return marketDeal;
        }

        public async Task<UserMarketDealEntity?> GetMarketDealByDealIdAndUserIdAsync(Guid dealId, Guid userId)
        {
            UserMarketDealEntity? marketDealEntity = await _dbContext.UserMarketDeals.Where(x => x.DealId == dealId && x.UserId == userId).AsNoTracking().FirstOrDefaultAsync();
            return marketDealEntity;
        }

        public async Task DeleteMarketDealByUserIdAsync(UserMarketDealEntity marketDeal)
        {
            var userMarketDealEntity = await _dbContext.UserMarketDeals
                .FirstOrDefaultAsync(x => x.UserId == marketDeal.UserId && x.DealId == marketDeal.DealId);

            if (userMarketDealEntity != null)
            {
                _dbContext.UserMarketDeals.Remove(userMarketDealEntity);
                await _dbContext.SaveChangesAsync();
            }
        }

    }
}