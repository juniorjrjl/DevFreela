using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        public GetUserByIdQueryHandler(IUserQueryRepository userQueryRepository)
        {
            _userQueryRepository = userQueryRepository;
        }

        public async Task<User> Handle(GetUserByIdQuery query, CancellationToken cancellationToken) => await _userQueryRepository.GetByIdAsync(query.Id);

    }
}
