using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;

namespace DevFreela.Application.Services.Implementations
{

    public class SkillService : ISkillService
    {

        private readonly DevFreelaDbContext _dbContext; 

        public SkillService(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Skill> GetAll() => _dbContext.Skills.ToList();

    }

}
