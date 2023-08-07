using DevFreela.Core.Entities;

namespace DevFreela.Application.Services.Interfaces
{
    
    public interface IUserService
    {
        
        User GetById(int id);

        User Create(User entity);

        User Login(User entity);

    }

}
