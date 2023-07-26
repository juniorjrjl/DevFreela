
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get(string query)
        {
            return Ok();
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post()
        {
            return CreatedAtAction(nameof(GetById), null);
        }

        [HttpPut("{id}")]
        public IActionResult Put()
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete()
        {
            return NoContent();
        }

        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id)
        {
            return NoContent();
        }

        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            return NoContent();
        }

        [HttpPut("{id}/finish")]
        public IActionResult Finish(int id)
        {
            return NoContent();
        }

    }
}
