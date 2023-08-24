using AutoMapper;
using DevFreela.API.InputModel;
using DevFreela.API.ViewModel;
using DevFreela.Application.Commands;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;

namespace DevFreela.API.Mappers
{
    
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<NewUserInputModel, CreateUserCommand>()
                .ConstructUsing(src => new CreateUserCommand(src.Name, src.Email, src.BirthDate, src.Password, src.Role, src.SkillsId));
            CreateMap<User, UserViewModel>();
            CreateMap<User, SavedUserViewModel>()
                .ForCtorParam(ctorParamName: nameof(SavedUserViewModel.Skills), opt => opt.MapFrom(src => src.UsersSkills));
            CreateMap<UserSkill, SavedUserSkillViewModel>()
                .ConstructUsing(src => new SavedUserSkillViewModel(src.SkillId, src.Skill.Description));
            CreateMap<CreateUserCommand, User>();
            CreateMap<LoginInputModel, UserLoginCommand>();
            CreateMap<CredentialDTO, CredentialViewModel>();
        }
    }

}
