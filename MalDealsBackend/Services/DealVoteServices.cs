using MalDealsBackend.Data;
using MalDealsBackend.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace MalDealsBackend.Services
{
    public class DealVoteServices(AppDbContext dbContext)
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<IEnumerable<DealVoteEntity>> GetDealVoteByDealIdAsync(Guid id)
        {
            IEnumerable<DealVoteEntity> dealVotes = await _dbContext.DealVotes.Where(x => x.DealId == id).AsNoTracking().ToListAsync();
            return dealVotes;
        }

         public async Task<IEnumerable<DealVoteEntity>> GetDealVoteByUserIdAsync(Guid id)
        {
            IEnumerable<DealVoteEntity> dealVotes = await _dbContext.DealVotes.Where(x => x.UserId == id).AsNoTracking().ToListAsync();
            return dealVotes;
        }

        public async Task<DealVoteEntity?> GetDealVoteByDealIdAndUserIdAsync(Guid dealId, Guid userId)
        {
            DealVoteEntity? dealVoteEntity = await _dbContext.DealVotes.Where(x => x.DealId == dealId && x.UserId == userId).AsNoTracking().FirstOrDefaultAsync();
            return dealVoteEntity;
        }

        public async Task<DealVoteEntity> CreateDealVoteAsync(DealVoteEntity dealVote)
        {
            DealVoteEntity dealVoteEntity = dealVote;
            _dbContext.DealVotes.Add(dealVoteEntity);
            await _dbContext.SaveChangesAsync();
            return dealVoteEntity;
        }

        public async Task DeleteDealVoteByUserIdAsync(DealVoteEntity dealVote)
        {
            DealVoteEntity dealVoteEntity = dealVote;
            _dbContext.DealVotes.Remove(dealVoteEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}