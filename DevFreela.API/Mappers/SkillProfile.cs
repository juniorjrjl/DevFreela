using AutoMapper;
using DevFreela.API.ViewModel;
using DevFreela.Core.Entities;

namespace DevFreela.API.Mappers
{
    public class SkillProfile : Profile
    {
        
        public SkillProfile()
        {
            CreateMap<Skill, SkillViewModel>();
        }

    }
}
