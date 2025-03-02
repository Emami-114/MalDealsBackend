using MalDeals.Data;
using MalDeals.Models.Entitys;
using Microsoft.EntityFrameworkCore;

namespace MalDeals.Services
{
    public class UserServices(AppDbContext dbContext)
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<IEnumerable<UserDataEntity>> GetUsersDataAsync()
        {
            IEnumerable<UserDataEntity> users = await _dbContext.UserData.AsNoTracking().ToListAsync();
            return users;
        }

        public async Task<UserDataEntity?> GetUserDataByIdAsync(Guid id)
        {
            UserDataEntity? user = await _dbContext.UserData.Where(x => x.UserId == id).AsNoTracking().FirstOrDefaultAsync();
            return user;
        }

        public async Task<UserDataEntity> CreateUserDataAsync(UserDataEntity userDto)
        {
            UserDataEntity user = userDto;
            _dbContext.UserData.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUserDataAsync(UserDataEntity userDto)
        {
            UserDataEntity user = userDto;
            _dbContext.UserData.Update(userDto);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserEntity?> GetUserByIdAsync(Guid id)
        {
            UserEntity? user = await _dbContext.Users.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            return user;
        }

        public async Task<UserEntity?> GetUserByEmailAsync(string email)
        {
            UserEntity? user = await _dbContext.Users.Where(x => x.Email == email).AsNoTracking().FirstOrDefaultAsync();
            return user;
        }

        public async Task<UserEntity> CreateUserAsync(UserEntity user)
        {
            UserEntity userEntity = user;
            _dbContext.Users.Add(userEntity);
            await _dbContext.SaveChangesAsync();
            return userEntity;
        }

        public async Task UpdateUserAsync(UserEntity user)
        {
            UserEntity userModel = user;
            _dbContext.Users.Update(userModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsExistsUserByEmailAsync(string email)
        {
            return await _dbContext.Users.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }

    }
}