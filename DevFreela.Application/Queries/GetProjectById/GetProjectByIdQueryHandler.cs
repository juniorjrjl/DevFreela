using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Project>
    {
        private readonly DevFreelaDbContext _dbContext;
        public GetProjectByIdQueryHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Project> Handle(GetProjectByIdQuery query, CancellationToken cancellationToken)
        {
            try
            {
                return await _dbContext.Projects.SingleAsync(p => p.Id == query.Id);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException($"Projeto {query.Id} não encontrado", ex);
            }
        }
    }
}