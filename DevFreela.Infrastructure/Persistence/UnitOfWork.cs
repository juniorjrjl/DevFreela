using DevFreela.Core.Repositories;
using DevFreela.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevFreela.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbContextTransaction _transaction;

        private readonly DevFreelaDbContext _dbContext;

        public IProjectRepository ProjectRepository { get; }

        public IProjectQueryRepository ProjectQueryRepository { get; }

        public IUserRepository UserRepository { get; }

        public IUserQueryRepository UserQueryRepository { get; }

        public ISkillQueryRepository SkillQueryRepository { get; }

        public UnitOfWork(DevFreelaDbContext dbContext, 
            IProjectRepository projectRepository,
            IProjectQueryRepository projectQueryRepository,
            IUserRepository userRepository,
            IUserQueryRepository userQueryRepository,
            SkillQueryRepository skillQueryRepository)
        {
            _dbContext = dbContext;
            ProjectRepository = projectRepository;
            ProjectQueryRepository = projectQueryRepository;
            UserRepository = userRepository;
            UserQueryRepository = userQueryRepository;
            SkillQueryRepository = skillQueryRepository;

        }

        public async Task<int> CompleteAsync() => await _dbContext.SaveChangesAsync();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

        public async Task BeginTransactionAsync() => _transaction = await _dbContext.Database.BeginTransactionAsync();

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }catch(Exception){
                await _transaction.RollbackAsync();
                throw;
            }
        }
    }
}