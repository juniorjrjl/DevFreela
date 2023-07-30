using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModel;
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

        public List<SkillViewModel> GetAll()
        {
            throw new NotImplementedException();
        }
    }

}