using AutoMapper;
using DevFreela.API.InputModel;
using DevFreela.API.ViewModel;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {

        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;

        public ProjectsController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(string query)
        {
            var projects = _projectService.GetAll(query);
            var viewModel = _mapper.Map<List<ProjectViewModel>>(projects);
            return Ok(viewModel);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _projectService.GetById(id);
            var viewModel = _mapper.Map<ProjectDetailsViewModel>(project);
            return Ok(viewModel);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewProjectInputModel inputModel)
        {
            var entity = _mapper.Map<Project>(inputModel);
            var saved = _projectService.Create(entity);
            var viewModel = _mapper.Map<CreatedProjectViewModel>(saved);
            return Created(nameof(GetById), viewModel);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateProjectInputModel inputModel)
        {
            var entity = _projectService.GetById(id);
            entity = _mapper.Map(inputModel, entity);
            var saved = _projectService.Update(entity);
            var viewModel = _mapper.Map<UpdatedProjectViewModel>(saved);
            return Ok(viewModel);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _projectService.Delete(id);
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateCommentInputModel inputModel)
        {
            _projectService.GetById(id);
            var entity = _mapper.Map<ProjectComment>(inputModel);
            entity.Id = id;
            _projectService.CreatedComment(entity);
            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            _projectService.Start(id);
            return NoContent();
        }

        [HttpPut("{id}/finish")]
        public IActionResult Finish(int id)
        {
            _projectService.Finish(id);
            return NoContent();
        }

    }
}
