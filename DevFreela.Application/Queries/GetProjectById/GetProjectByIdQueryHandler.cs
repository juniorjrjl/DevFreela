using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Project>
    {
        private readonly IProjectQueryRepository _projectQueryRepository;
        public GetProjectByIdQueryHandler(IProjectQueryRepository projectQueryRepository)
        {
            _projectQueryRepository = projectQueryRepository;
        }

        public async Task<Project> Handle(GetProjectByIdQuery query, CancellationToken cancellationToken) => await _projectQueryRepository.GetByIdAsync(query.Id);

    }
}
