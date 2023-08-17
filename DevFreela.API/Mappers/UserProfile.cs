using AutoMapper;
using DevFreela.API.InputModel;
using DevFreela.API.ViewModel;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.Entities;

namespace DevFreela.API.Mappers
{
    
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<NewUserInputModel, CreateUserCommand>();
            CreateMap<User, UserViewModel>();
            CreateMap<User, SavedUserViewModel>();
            CreateMap<CreateUserCommand, User>();
        }
    }

}
