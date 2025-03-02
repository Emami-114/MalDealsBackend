using MalDeals.Data;
using MalDeals.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace MalDeals.Services
{
    public class TagServices(AppDbContext dbContext)
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<IEnumerable<TagEntity>> GetTagsAsync()
        {
            IEnumerable<TagEntity> tags = await _dbContext.Tags.AsNoTracking().ToListAsync();
            return tags;
        }
        public async Task<TagEntity?> GetTagByIdAsync(Guid id)
        {
            TagEntity? tag = await _dbContext.Tags.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return tag;
        }

        public async Task<TagEntity> CreateTagAsync(TagEntity tagModel)
        {
            TagEntity tag = tagModel;
            _dbContext.Tags.Add(tag);
            await _dbContext.SaveChangesAsync();
            return tag;
        }

        public async Task UpdateTagAsync(TagEntity tagModel)
        {
            TagEntity updateTag = tagModel;
            _dbContext.Tags.Update(updateTag);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteTagAsync(TagEntity tag)
        {
            _dbContext.Tags.Remove(tag);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> TagExistsByTitleAsync(string title)
        {
            return await _dbContext.Tags.AnyAsync(x => x.Title.ToLower() == title.ToLower());
        }
    }
}