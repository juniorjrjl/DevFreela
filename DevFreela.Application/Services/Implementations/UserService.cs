using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations
{

    public class UserService : IUserService
    {

        private readonly DevFreelaDbContext _dbContext;

        public UserService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User Create(User entity)
        {
            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public User GetById(int id)
        {
            try
            {
                return _dbContext.Users.Single(p => p.Id == id);
            }catch(ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Usuário {id} não encontrado", ex);
            }
        }

        public User Login(User entity)
        {
            throw new NotImplementedException();
        }

    }

}
