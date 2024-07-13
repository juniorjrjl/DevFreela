using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Queries.GetUserById;

public record GetUserByIdQuery(int Id) : IRequest<User>;
