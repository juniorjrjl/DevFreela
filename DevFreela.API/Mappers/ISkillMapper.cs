using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevFreela.API.ViewModel;
using DevFreela.Core.Entities;

namespace DevFreela.API.Mappers;

public interface ISkillMapper
{

    ICollection<SkillViewModel> ToViewModel(ICollection<Skill> entities);

}
