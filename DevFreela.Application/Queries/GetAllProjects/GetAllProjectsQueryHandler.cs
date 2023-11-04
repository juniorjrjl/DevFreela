using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, List<Project>>
    {
        private readonly IProjectQueryRepository _projectQueryRepository;
        public GetAllProjectsQueryHandler(IProjectQueryRepository projectQueryRepository)
        {
            _projectQueryRepository = projectQueryRepository;
        }

        public async Task<List<Project>> Handle(GetAllProjectsQuery query, CancellationToken cancellationToken) => await _projectQueryRepository.GetAllAsync(query.Query);

    }
}
