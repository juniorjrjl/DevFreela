using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserQueryRepository : IUserQueryRepository
    {
        private readonly DevFreelaDbContext _dbContext;

        public UserQueryRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            try
            {
                return await  _dbContext.Users.SingleAsync(p => p.Id == id);
            }catch(ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Usuário {id} não encontrado", ex);
            }
        }

        public async Task<User> GetByEmailAndPasswordAsync(string email, string passwordHash)
        {
            try
            {
                return await _dbContext.Users.SingleAsync(p => p.Email == email && p.Password == passwordHash);
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException($"Não foi encontrado um usuário com as credenciais informadas", ex);
            }
        }

    }

}
