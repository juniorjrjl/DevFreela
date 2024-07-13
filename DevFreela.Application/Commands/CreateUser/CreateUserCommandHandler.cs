using DevFreela.Application.Mapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.Core.Services;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.CreateUser;


public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserMapper _mapper;
    private readonly IAuthService _authService;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IUserMapper mapper, IAuthService authService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _authService = authService;
    }

    public async Task<User> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        command = command with { Password = _authService.ComputeSha256Hash(command.Password) };

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var userRoles = await ToUserRoleAsync(command.Roles);
            var userSkills = ToUserSkills(command.SkillsId);
            var entity = _mapper.ToEntity(command, userRoles, userSkills);

            entity = await _unitOfWork.UserRepository.AddAsync(entity);
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitAsync();
            await _unitOfWork.IncludeListAsync(entity, u => u.UsersSkills, u => u.Skill);
            await _unitOfWork.IncludeListAsync(entity, u => u.UsersRoles, u => u.Role);
            return entity;
        } catch (Exception)
        {
            await _unitOfWork.RollBackAsync();
            throw;
        }
    }

    private async Task<ICollection<UserRole>> ToUserRoleAsync(ICollection<RoleNameEnum> rolesNames)
    {
        if (rolesNames is null || !rolesNames.Any())
        {
            return new List<UserRole>();
        }
        ICollection<Role> roles = new List<Role>();
        foreach (RoleNameEnum role in rolesNames)
        {
            var roleEntity = await _unitOfWork.RoleQueryRepository.GetByNameAsync(role);
            roles.Add(roleEntity);
        }
        return roles.Select(r => new UserRole(r.Id)).ToList();
    }

    private static ICollection<UserSkill> ToUserSkills(ICollection<int>? skillsId)
    {
        ICollection<UserSkill> userSkills = new List<UserSkill>();
        if (skillsId is null || !skillsId.Any())
        {
            return userSkills;
        }
        userSkills = skillsId.Select(id => new UserSkill(id)).ToList();
        return userSkills;
    }

}
