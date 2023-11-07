using AutoMapper;
using DevFreela.API.InputModel;
using DevFreela.API.ViewModel;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateProjectComment;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Commands.FinishProject;
using DevFreela.Application.Commands.StartProject;
using DevFreela.Application.Commands.UpdateProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using DevFreela.Core.Persistence.model;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ProjectsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "CLIENT, FREELANCER, ADMIN")]
        public async Task<IActionResult> Get(GetAllProjectsQuery getAllProjectsQueryquery)
        {
            var entity = await _mediator.Send(getAllProjectsQueryquery);
            var viewModel = _mapper.Map<PaginationResult<ProjectViewModel>>(entity);
            return Ok(viewModel);
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = "CLIENT, FREELANCER, ADMIN")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _mediator.Send(new GetProjectByIdQuery(id));
            var viewModel = _mapper.Map<ProjectDetailsViewModel>(entity);
            return Ok(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "CLIENT, ADMIN")]
        public async Task<IActionResult> Post([FromBody] NewProjectInputModel inputModel)
        {
            var command = _mapper.Map<CreateProjectCommand>(inputModel);
            var saved = await _mediator.Send(command);
            var viewModel = _mapper.Map<CreatedProjectViewModel>(saved);
            return Created(nameof(GetById), viewModel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "CLIENT, ADMIN")]
        public async Task<IActionResult> Put(int id, [FromBody]UpdateProjectInputModel inputModel)
        {
            var command = _mapper.Map<UpdateProjectCommand>(inputModel);
            var saved = await _mediator.Send(command with { Id = id });
            var viewModel = _mapper.Map<UpdatedProjectViewModel>(saved);
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
            var command = _mapper.Map<CreateProjectCommentCommand>(inputModel);
            await _mediator.Send(command with { ProjectId = id});
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
}
