using AutoMapper;
using DevFreela.API.InputModel;
using DevFreela.API.ViewModel;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public UsersController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _mediator.Send(new GetUserByIdQuery(id));
            var viewModel = _mapper.Map<UserViewModel>(entity);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]NewUserInputModel inputModel)
        {
            var command = _mapper.Map<CreateUserCommand>(inputModel);
            var saved = await _mediator.Send(command);
            var viewModel = _mapper.Map<SavedUserViewModel>(saved);
            return Created(nameof(GetById), viewModel);
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            return CreatedAtAction(nameof(GetById), null);
        }

    }
}
