using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, ICollection<Project>>
    {
        private readonly IProjectQueryRepository _projectQueryRepository;
        public GetAllProjectsQueryHandler(IProjectQueryRepository projectQueryRepository)
        {
            _projectQueryRepository = projectQueryRepository;
        }

        public async Task<ICollection<Project>> Handle(GetAllProjectsQuery query, CancellationToken cancellationToken) => await _projectQueryRepository.GetAllAsync();

    }
}
