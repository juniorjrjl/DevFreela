
using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IUserQueryRepository
    {

        Task<User> GetByIdAsync(int id);

        Task<User> GetByEmailAndPasswordAsync(string email, string passwordHash);

    }
}
