using AutoMapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser
{

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IAuthService authService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _authService = authService;
        }

        public async Task<User> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            command = command with { Password = _authService.ComputeSha256Hash(command.Password) };
            var entity = _mapper.Map<User>(command);
            if (command.SkillsId is not null && command.SkillsId.Any()){
                entity.UsersSkills = command.SkillsId.Select(id => new UserSkill{SkillId = id}).ToList();
            }
            return await _userRepository.AddAsync(entity);
        }
    }

}
