
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

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public IActionResult Get(string query)
        {
            var projects = _projectService.GetAll(query);
            var viewModel = projects.Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt)).ToList();
            return Ok(viewModel);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var project = _projectService.GetById(id);
            var viewModel = new ProjectDetailsViewModel(
                project.Id, 
                project.Title, 
                project.Description, 
                project.TotalCost, 
                project.CreatedAt, 
                project.FinishedAt
            );
            return Ok(viewModel);
        }

        [HttpPost]
        public IActionResult Post([FromBody] NewProjectInputModel inputModel)
        {
            var entity = new Project(inputModel.Title, inputModel.Description, inputModel.ClientId, inputModel.FreelancerId, inputModel.TotalCost);
            var saved = _projectService.Create(entity);
            var viewModel = new CreatedProjectViewModel(saved.Id, saved.Title, saved.Description, saved.ClientId, saved.FreelancerId, saved.TotalCost);
            return CreatedAtAction(nameof(GetById), viewModel);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]UpdateProjectInputModel inputModel)
        {
            var entity = _projectService.GetById(id);
            entity.Title = inputModel.Title;
            entity.Description = inputModel.Description;
            entity.TotalCost = inputModel.TotalCost;
            var saved = _projectService.Update(entity);
            var viewModel = new UpdatedProjectViewModel(saved.Id, saved.Title, saved.Description, saved.TotalCost);
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
            var entity = new ProjectComment(inputModel.Content, id, inputModel.UserId);
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
