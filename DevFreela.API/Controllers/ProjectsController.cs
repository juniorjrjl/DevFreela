using DevFreela.API.InputModel;
using DevFreela.API.Mappers;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers;

[Route("api/projects")]
public class ProjectsController : ControllerBase
{

    private readonly IProjetcMapper _mapper;
    private readonly IMediator _mediator;

    public ProjectsController(IProjetcMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "CLIENT, FREELANCER, ADMIN")]
    public async Task<IActionResult> Get(GetAllProjectsQuery getAllProjectsQueryquery)
    {
        var entity = await _mediator.Send(getAllProjectsQueryquery);
        var viewModel = _mapper.ToPagedViewModel(entity);
        return Ok(viewModel);
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "CLIENT, FREELANCER, ADMIN")]
    public async Task<IActionResult> GetById(int id)
    {
        var entity = await _mediator.Send(new GetProjectByIdQuery(id));
        var viewModel = _mapper.ToDetailsViewModel(entity);
        return Ok(viewModel);
    }

    [HttpPost]
    [Authorize(Roles = "CLIENT, ADMIN")]
    public async Task<IActionResult> Post([FromBody] NewProjectInputModel inputModel)
    {
        var command = _mapper.ToInputModel(inputModel);
        var saved = await _mediator.Send(command);
        var viewModel = _mapper.ToCreatedViewModel(saved);
        return Created(nameof(GetById), viewModel);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "CLIENT, ADMIN")]
    public async Task<IActionResult> Put(int id, [FromBody]UpdateProjectInputModel inputModel)
    {
        var command = _mapper.ToCommand(inputModel, id);
        var saved = await _mediator.Send(command);
        var viewModel = _mapper.ToViewModel(saved);
        return Ok(viewModel);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "CLIENT, ADMIN")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteProjectCommand(id));
        return NoContent();
    }

    [HttpPost("{id}/comments")]
    [Authorize(Roles = "CLIENT, FREELANCER, ADMIN")]
    public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentInputModel inputModel)
    {
        var command = _mapper.ToCommand(inputModel, id);
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}/start")]
    [Authorize(Roles = "CLIENT, ADMIN")]
    public async Task<IActionResult> Start(int id)
    {
        await _mediator.Send(new StartProjectCommand(id));
        return NoContent();
    }

    [HttpPut("{id}/finish")]
    [Authorize(Roles = "CLIENT, FREELANCER, ADMIN")]
    public async Task<IActionResult> Finish(int id)
    {
        await _mediator.Send(new FinishProjectCommand(id));
        return NoContent();
    }

}
