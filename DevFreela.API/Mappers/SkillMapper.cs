using DevFreela.API.ViewModel;
using DevFreela.Core.Entities;

namespace DevFreela.API.Mappers;

public class SkillMapper : ISkillMapper
{
    public ICollection<SkillViewModel> ToViewModel(ICollection<Skill> entities) => entities.Select(e => ToViewModel(e)).ToList();

    private static SkillViewModel ToViewModel(Skill entity) => new (entity.Id, entity.Description);

}
