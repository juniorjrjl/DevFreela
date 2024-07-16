using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class UserRepository(DevFreelaDbContext dbContext) : IUserRepository
{
    private readonly DevFreelaDbContext _dbContext = dbContext;

    public async Task<User> AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        return user;
    }
}
