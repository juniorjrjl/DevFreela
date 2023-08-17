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
    }

}
