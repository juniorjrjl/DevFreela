using System.Linq.Expressions;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevFreela.Infrastructure.Persistence;

public class UnitOfWork(DevFreelaDbContext dbContext,
    IProjectRepository projectRepository,
    IProjectQueryRepository projectQueryRepository,
    IUserRepository userRepository,
    IUserQueryRepository userQueryRepository,
    ISkillQueryRepository skillQueryRepository,
    IRoleRepository roleRepository,
    IRoleQueryRepository roleQueryRepository) : IUnitOfWork, IDisposable
{
    private IDbContextTransaction? _transaction;

    private readonly DevFreelaDbContext _dbContext = dbContext;

    public IProjectRepository ProjectRepository { get; } = projectRepository;

    public IProjectQueryRepository ProjectQueryRepository { get; } = projectQueryRepository;

    public IUserRepository UserRepository { get; } = userRepository;

    public IUserQueryRepository UserQueryRepository { get; } = userQueryRepository;

    public ISkillQueryRepository SkillQueryRepository { get; } = skillQueryRepository;

    public IRoleRepository RoleRepository { get; } = roleRepository;

    public IRoleQueryRepository RoleQueryRepository { get; } = roleQueryRepository;

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

    public async Task IncludeAsync<TEntity, TProperty>(
        TEntity entity, 
        Expression<Func<TEntity, TProperty?>> propertyExpression
    )
        where TProperty : class
        where TEntity : class
    => await _dbContext.Entry(entity).Reference(propertyExpression).LoadAsync();
}
