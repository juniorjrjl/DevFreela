using AutoMapper;
using DevFreela.API.InputModel;
using DevFreela.API.ViewModel;
using DevFreela.Application.Commands;
using DevFreela.Application.Commands.CreateUser;
using DevFreela.Application.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [Authorize]
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
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]NewUserInputModel inputModel)
        {
            var command = _mapper.Map<CreateUserCommand>(inputModel);
            var saved = await _mediator.Send(command);
            var viewModel = _mapper.Map<SavedUserViewModel>(saved);
            return Created(nameof(GetById), viewModel);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginInputModel inputModel)
        {
            var command = _mapper.Map<UserLoginCommand>(inputModel);
            var dto = await _mediator.Send(command);
            var viewModel = _mapper.Map<CredentialViewModel>(dto);
            return Created("", viewModel);
        }

    }
}
