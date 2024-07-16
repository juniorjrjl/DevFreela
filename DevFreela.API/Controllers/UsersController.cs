using DevFreela.API.InputModel;
using DevFreela.API.Mappers;
using DevFreela.Application.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController(IUserMapper mapper, IMediator mediator) : ControllerBase
{
    private readonly IUserMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var entity = await _mediator.Send(new GetUserByIdQuery(id));
        var viewModel = _mapper.ToGetByIdViewModel(entity);
        return Ok(viewModel);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody]NewUserInputModel inputModel)
    {
        var command = _mapper.ToCommand(inputModel);
        var saved = await _mediator.Send(command);
        var viewModel = _mapper.ToPostViewModel(saved);
        return Created(nameof(GetById), viewModel);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginInputModel inputModel)
    {
        var command = _mapper.ToCommand(inputModel);
        var dto = await _mediator.Send(command);
        var viewModel = _mapper.ToLoginViewModel(dto);
        return Created("", viewModel);
    }

}
