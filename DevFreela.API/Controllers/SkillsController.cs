using DevFreela.API.Mappers;
using DevFreela.Application.Queries.GetAllSkills;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[ApiController]
[Route("api/skills")]
[Authorize]
public class SkillsController(IMediator mediator, ISkillMapper mapper) : ControllerBase
{

    private readonly IMediator _mediator = mediator;
    private readonly ISkillMapper _mapper = mapper;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var entity = await _mediator.Send(new GetAllSkillsQuery());
        var viewModel = _mapper.ToViewModel(entity);
        return Ok(viewModel);
    }

}
