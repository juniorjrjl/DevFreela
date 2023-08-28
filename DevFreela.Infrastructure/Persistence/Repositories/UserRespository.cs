using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            await _dbContext.Entry(user).Collection(u => u.UsersSkills).Query().Include(u => u.Skill).LoadAsync();
            await _dbContext.Entry(user).Collection(u => u.UsersRoles).Query().Include(u => u.Role).LoadAsync();
            return user;
        }
    }
}
