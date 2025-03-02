using MalDealsBackend.Data;
using MalDealsBackend.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace MalDealsBackend.Services
{
    public class CategoryServices(AppDbContext dbContext)
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<IEnumerable<CategoryEntity>> GetCategoriesAsync()
        {
            IEnumerable<CategoryEntity> categories = await _dbContext.Categories.AsNoTracking().ToListAsync();
            return categories;
        }

        public async Task<CategoryEntity?> GetCategoryByIdAsync(Guid id)
        {
            CategoryEntity? category = await _dbContext.Categories.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return category;
        }

        public async Task<CategoryEntity> CreateCategoryAsync(CategoryEntity categoryModel)
        {
            CategoryEntity category = categoryModel;
            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategoryAsync(CategoryEntity category)
        {
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(CategoryEntity category)
        {
            CategoryEntity updateCategory = category;
            _dbContext.Categories.Update(updateCategory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> CategoryExistsByTitleAsync(string title)
        {
            return await _dbContext.Categories.AnyAsync(x => x.Title.ToLower() == title.ToLower());
        }

    }
}