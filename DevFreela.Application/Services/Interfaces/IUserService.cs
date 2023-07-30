using DevFreela.Application.InputModel;
using DevFreela.Application.ViewModel;

namespace DevFreela.Application.Services.Interfaces
{
    
    public interface IUserService
    {
        
        UserViewModel GetById(int id);

        SavedUserViewModel Create(NewUserInputModel inputModel);

        CredentialViewModel Login(CredentialInputModel inputModel);

    }

}
