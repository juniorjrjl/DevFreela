using DevFreela.API.InputModel;
using DevFreela.API.ViewModel;
using DevFreela.Application.Commands;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;

namespace DevFreela.API.Mappers;

public interface IUserMapper
{
    CreateUserCommand ToCommand(NewUserInputModel command);
    UserLoginCommand ToCommand(LoginInputModel command);
    UserViewModel ToGetByIdViewModel(User entity);
    SavedUserViewModel ToPostViewModel(User entity);
    CredentialViewModel ToLoginViewModel(CredentialDTO dto);

}
