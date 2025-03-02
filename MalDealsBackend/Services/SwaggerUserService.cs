using MalDealsBackend.Data;
using MalDealsBackend.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace MalDealsBackend.Services
{
    public class SwaggerUserService
    {
        private readonly AppDbContext _dbContext;

        public SwaggerUserService(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<SwaggerUserEntity?> GetSwaggerUserAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            return await _dbContext.SwaggerUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}