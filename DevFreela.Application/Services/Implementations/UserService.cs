using DevFreela.Application.InputModel;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModel;
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

        public SavedUserViewModel Create(NewUserInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public CredentialViewModel Login(CredentialInputModel inputModel)
        {
            throw new NotImplementedException();
        }
        
    }

}