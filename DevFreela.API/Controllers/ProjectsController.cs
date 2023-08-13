using AutoMapper;
using DevFreela.API.InputModel;
using DevFreela.API.ViewModel;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateProjectComment;
using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Application.Queries.GetAllProjects;
using DevFreela.Application.Queries.GetProjectById;
using MediatR;
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
        public async Task<IActionResult> Get(string query)
        {
            var entity = await _mediator.Send(new GetAllProjectsQuery(query));
            var viewModel = _mapper.Map<List<ProjectViewModel>>(entity);
            return Ok(viewModel);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = await _mediator.Send(new GetProjectByIdQuery(id));
            var viewModel = _mapper.Map<ProjectDetailsViewModel>(entity);
            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewProjectInputModel inputModel)
        {
            var command = _mapper.Map<CreateProjectCommand>(inputModel);
            var saved = await _mediator.Send(command);
            var viewModel = _mapper.Map<CreatedProjectViewModel>(saved);
            return Created(nameof(GetById), viewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UpdateProjectInputModel inputModel)
        {
            var command = _mapper.Map<CreateProjectCommand>(inputModel);
            var saved = await _mediator.Send(command);
            var viewModel = _mapper.Map<UpdatedProjectViewModel>(saved);
            return Ok(viewModel);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteProjectCommand(id));
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostComment(int id, [FromBody] CreateCommentInputModel inputModel)
        {
            await _mediator.Send(new GetProjectByIdQuery(id));
            var command = _mapper.Map<CreateProjectCommentCommand>(inputModel);
            command.ProjectId = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}/start")]
        public async Task<IActionResult> Start(int id)
        {
            await _mediator.Send(new StartProjectCommand(id));
            return NoContent();
        }

        [HttpPut("{id}/finish")]
        public async Task<IActionResult> Finish(int id)
        {
            await _mediator.Send(new FinishProjectCommand(id));
            return NoContent();
        }

    }
}
