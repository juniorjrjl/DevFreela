using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdQueryHandler(IProjectQueryRepository projectQueryRepository) : IRequestHandler<GetProjectByIdQuery, Project>
{
    private readonly IProjectQueryRepository _projectQueryRepository = projectQueryRepository;

    public async Task<Project> Handle(GetProjectByIdQuery query, CancellationToken cancellationToken) => await _projectQueryRepository.GetByIdAsync(query.Id);

}
