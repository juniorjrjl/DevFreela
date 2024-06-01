using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevFreela.Core.Repositories;

namespace DevFreela.Infrastructure.Persistence
{
    public interface IUnitOfWork
    {
        
        IProjectRepository ProjectRepository { get;  }

        IProjectQueryRepository ProjectQueryRepository { get; }

        IUserRepository UserRepository { get;  }

        IUserQueryRepository UserQueryRepository { get; }

        ISkillQueryRepository SkillQueryRepository { get; }

        Task<int> CompleteAsync();

        Task BeginTransactionAsync();

        Task CommitAsync();

    }
}