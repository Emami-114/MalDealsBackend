using MalDealsBackend.Data;
using MalDealsBackend.Models;
using MalDealsBackend.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace MalDealsBackend.Services
{
    public class DealServices(AppDbContext dbContext)
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<IEnumerable<DealEntity>> GetDealsAsync(DealFilterQuery queryParameters)
        {
            IQueryable<DealEntity> query = _dbContext.Deals.AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryParameters.SearchTerm))
            {
                string searchTerm = $"%{queryParameters.SearchTerm.ToLower()}%";

                query = query.Where(x =>
                EF.Functions.Like(x.Title.ToLower(), searchTerm) ||
                x.Tags != null && x.Tags.Any(tag => EF.Functions.Like(tag.ToLower(), searchTerm)) ||
                x.Categories != null && x.Categories.Any(cat => EF.Functions.Like(cat.ToLower(), searchTerm)) ||
                EF.Functions.Like((x.Provider ?? "").ToLower(), searchTerm)
            );
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(queryParameters.Category))
                {
                    query = query.Where(x => x.Categories != null && x.Categories.Any(c => c.ToLower() == queryParameters.Category.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(queryParameters.Provider))
                {
                    query = query.Where(x => (x.Provider ?? "").ToLower() == queryParameters.Provider.ToLower());
                }

                if (!string.IsNullOrWhiteSpace(queryParameters.Tag))
                {
                    query = query.Where(x => x.Tags != null && x.Tags.Any(t => t.ToLower() == queryParameters.Tag.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(queryParameters.Title))
                {
                    string titleSearch = $"%{queryParameters.Title.ToLower()}%";
                    query = query.Where(x => EF.Functions.Like(x.Title.ToLower(), titleSearch));
                }
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)queryParameters.Limit);
            var deals = await query.Skip((queryParameters.Page - 1) * queryParameters.Limit).Take(queryParameters.Limit).ToListAsync();

            return deals;
        }

        public async Task<DealEntity?> GetDealByIdAsync(Guid id)
        {
            DealEntity? deal = await _dbContext.Deals.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return deal;
        }

        public async Task<DealEntity> CreateDealAsync(DealEntity dealModel)
        {
            DealEntity deal = dealModel;
            _dbContext.Deals.Add(deal);
            await _dbContext.SaveChangesAsync();
            return deal;
        }

        public async Task DeleteDealAsync(DealEntity deal)
        {
            _dbContext.Deals.Remove(deal);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateDealAsync(DealEntity deal)
        {
            DealEntity updateDeal = deal;
            _dbContext.Deals.Update(updateDeal);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DealExistsByTitleAsync(string title)
        {
            return await _dbContext.Deals.AnyAsync(x => x.Title.ToLower() == title.ToLower());
        }
    }
}