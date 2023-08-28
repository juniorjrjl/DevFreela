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
                .ConstructUsing(src => new CreateUserCommand(src.Name, src.Email, src.BirthDate, src.Password, src.Roles, src.SkillsId));
            CreateMap<User, UserViewModel>();
            CreateMap<User, SavedUserViewModel>()
                .ForCtorParam(ctorParamName: nameof(SavedUserViewModel.Skills), opt => opt.MapFrom(src => src.UsersSkills))
                .ForCtorParam(ctorParamName: nameof(SavedUserViewModel.Roles), opt => opt.MapFrom(src => src.UsersRoles));
            CreateMap<UserSkill, SavedUserSkillViewModel>()
                .ConstructUsing(src => new SavedUserSkillViewModel(src.SkillId, src.Skill.Description));
            CreateMap<UserRole, SavedUserRoleViewModel>()
                .ConstructUsing(src => new SavedUserRoleViewModel(src.RoleId, src.Role.Name.ToString()));
            CreateMap<CreateUserCommand, User>();
            CreateMap<LoginInputModel, UserLoginCommand>();
            CreateMap<CredentialDTO, CredentialViewModel>();
        }
    }

}
