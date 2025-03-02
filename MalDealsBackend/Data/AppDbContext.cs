using MalDeals.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace MalDeals.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<DealEntity> Deals { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ProviderEntity> Providers { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserDataEntity> UserData { get; set; }
        public DbSet<UserMarketDealEntity> UserMarketDeals { get; set; }
        public DbSet<DealVoteEntity> DealVotes { get; set; }
        public DbSet<SwaggerUserEntity> SwaggerUsers { get; set; }
    }
}