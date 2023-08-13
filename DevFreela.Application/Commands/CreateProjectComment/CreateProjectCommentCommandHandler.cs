using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateProjectComment
{

    public class CreateProjectCommentCommandHandler : IRequestHandler<CreateProjectCommentCommand, ProjectComment>
    {

        private readonly DevFreelaDbContext _dbContext; 

        public CreateProjectCommentCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProjectComment> Handle(CreateProjectCommentCommand command, CancellationToken cancellationToken)
        {
            var entity = new ProjectComment
            {
                Id = command.Id,
                Comment = command.Comment,
                ProjectId = command.ProjectId,
                UserId = command.UserId
            };
            await _dbContext.ProjectsComments.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }

}
