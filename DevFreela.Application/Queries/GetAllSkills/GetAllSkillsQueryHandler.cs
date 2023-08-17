using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllSkills
{
    public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, List<Skill>>
    {
        
        private readonly ISkillQueryRepository _skillQueryRepository;
        public GetAllSkillsQueryHandler(ISkillQueryRepository skillQueryRepository)
        {
            _skillQueryRepository = skillQueryRepository;
        }

        public async Task<List<Skill>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken) => await _skillQueryRepository.GetAllAsync();

    }
}
