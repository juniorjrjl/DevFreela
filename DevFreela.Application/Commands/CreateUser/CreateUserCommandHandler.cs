using AutoMapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser
{

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<User> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<User>(command);
            if (command.SkillsId is not null && command.SkillsId.Any()){
                entity.UsersSkills = command.SkillsId.Select(id => new UserSkill{SkillId = id}).ToList();
            }
            return await _userRepository.AddAsync(entity);
        }
    }

}
