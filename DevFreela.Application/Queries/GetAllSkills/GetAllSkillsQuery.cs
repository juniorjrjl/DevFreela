using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Queries.GetAllSkills;

public class GetAllSkillsQuery : IRequest<ICollection<Skill>>{}

