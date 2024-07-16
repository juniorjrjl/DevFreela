using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllSkills;

public class GetAllSkillsQueryHandler(ISkillQueryRepository skillQueryRepository) : IRequestHandler<GetAllSkillsQuery, ICollection<Skill>>
{
    
    private readonly ISkillQueryRepository _skillQueryRepository = skillQueryRepository;

    public async Task<ICollection<Skill>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken) => await _skillQueryRepository.GetAllAsync();

}
