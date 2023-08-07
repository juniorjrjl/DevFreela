using AutoMapper;
using DevFreela.API.InputModel;
using DevFreela.API.ViewModel;
using DevFreela.Core.Entities;

namespace DevFreela.API.Mappers
{
    
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<int, Skill>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src));
            CreateMap<NewUserInputModel, User>();
            CreateMap<User, SavedUserViewModel>();
        }
    }

}