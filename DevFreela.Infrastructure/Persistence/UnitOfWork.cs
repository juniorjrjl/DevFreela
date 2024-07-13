using System.Linq.Expressions;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevFreela.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private IDbContextTransaction? _transaction;

    private readonly DevFreelaDbContext _dbContext;

    public IProjectRepository ProjectRepository { get; }

    public IProjectQueryRepository ProjectQueryRepository { get; }

    public IUserRepository UserRepository { get; }

    public IUserQueryRepository UserQueryRepository { get; }

    public ISkillQueryRepository SkillQueryRepository { get; }

    public IRoleRepository RoleRepository { get; }

    public IRoleQueryRepository RoleQueryRepository { get; }

    public UnitOfWork(DevFreelaDbContext dbContext, 
        IProjectRepository projectRepository,
        IProjectQueryRepository projectQueryRepository,
        IUserRepository userRepository,
        IUserQueryRepository userQueryRepository,
        ISkillQueryRepository skillQueryRepository,
        IRoleRepository roleRepository,
        IRoleQueryRepository roleQueryRepository)
    {
        _dbContext = dbContext;
        ProjectRepository = projectRepository;
        ProjectQueryRepository = projectQueryRepository;
        UserRepository = userRepository;
        UserQueryRepository = userQueryRepository;
        SkillQueryRepository = skillQueryRepository;
        RoleRepository = roleRepository;
        RoleQueryRepository = roleQueryRepository;

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
            ArgumentNullException.ThrowIfNull(_transaction, "Start Transaction Before commit it.");
            await _transaction.CommitAsync();
        }catch(Exception){
            if (_transaction is not null)
            {
                await _transaction.RollbackAsync();
            }
            throw;
        }
    }

    public async Task RollBackAsync()
    {
        ArgumentNullException.ThrowIfNull(_transaction, "Start Transaction Before commit it.");
        await _transaction.RollbackAsync();
    }
    public async Task IncludeListAsync<TEntity, TProperty, TPropertyPath>(
        TEntity entity, Expression<Func<TEntity, 
        IEnumerable<TProperty>>> propertyExpression,
        Expression<Func<TProperty, TPropertyPath>> navigationPropertyPath
    ) 
        where TProperty : class
        where TEntity : class
    => await _dbContext.Entry(entity).Collection(propertyExpression).Query().Include(navigationPropertyPath).LoadAsync();

}
