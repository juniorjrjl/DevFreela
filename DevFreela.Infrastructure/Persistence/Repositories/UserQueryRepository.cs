using DevFreela.Core.Entities;
using DevFreela.Core.Exceptions;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class UserQueryRepository(DevFreelaDbContext dbContext) : IUserQueryRepository
{
    private readonly DevFreelaDbContext _dbContext = dbContext;

    public async Task<User> GetByIdAsync(int id)
    {
        try
        {
            return await  _dbContext.Users.SingleAsync(p => p.Id == id);
        }catch(InvalidOperationException ex)
        {
            throw new NotFoundException($"Usuário {id} não encontrado", ex);
        }
    }

    public async Task<User> GetByEmailAndPasswordAsync(string email, string passwordHash)
    {
        try
        {
            return await _dbContext.Users.SingleAsync(p => p.Email == email && p.Password == passwordHash);
        }
        catch (InvalidOperationException ex)
        {
            throw new NotFoundException($"Não foi encontrado um usuário com as credenciais informadas", ex);
        }
    }

}
