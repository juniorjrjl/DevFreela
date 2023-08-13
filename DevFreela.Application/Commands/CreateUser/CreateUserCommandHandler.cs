using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser
{

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {

        private readonly DevFreelaDbContext _dbContext; 

        public CreateUserCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var entity = new User
            {
                Name = command.Name,
                Email = command.Email,
                BirthDate = command.BirthDate
            };
            if (command.SkillsId is not null && command.SkillsId.Any()){
                entity.UsersSkills = command.SkillsId.Select(id => new UserSkill{SkillId = id}).ToList();
            }
            await _dbContext.Users.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }

}
