using System.Linq.Expressions;
using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence;

public interface IUnitOfWork
{
    
    IProjectRepository ProjectRepository { get;  }

    IProjectQueryRepository ProjectQueryRepository { get; }

    IUserRepository UserRepository { get;  }

    IUserQueryRepository UserQueryRepository { get; }

    ISkillQueryRepository SkillQueryRepository { get; }

    IRoleRepository RoleRepository { get; }

    IRoleQueryRepository RoleQueryRepository { get; }

    Task<int> CompleteAsync();

    Task BeginTransactionAsync();

    Task CommitAsync();

    Task RollBackAsync();

    Task IncludeListAsync<TEntity, TProperty, TPropertyPath>(
        TEntity entity, 
        Expression<Func<TEntity, IEnumerable<TProperty>>> 
        propertyExpression,Expression<Func<TProperty, TPropertyPath>> navigationPropertyPath
    ) 
    where TProperty : 
    class where TEntity : class;

    Task IncludeAsync<TEntity, TProperty>(
        TEntity entity, 
        Expression<Func<TEntity, TProperty?>> propertyExpression
    )
    where TProperty : class
    where TEntity : class;

}