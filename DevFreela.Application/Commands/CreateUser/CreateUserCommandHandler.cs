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
        private readonly IRoleQueryRepository _roleQueryRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public CreateUserCommandHandler(IUserRepository userRepository, IRoleQueryRepository roleQueryRepository, IMapper mapper, IAuthService authService)
        {
            _userRepository = userRepository;
            _roleQueryRepository = roleQueryRepository;
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
            ICollection<Role> roles = command.Roles.Select(async r => await _roleQueryRepository.GetByNameAsync(r)).Select(r => r.Result).ToList();
            entity.UsersRoles = roles.Select(r => new UserRole{RoleId = r.Id}).ToList();
            return await _userRepository.AddAsync(entity);
        }
    }

}
