
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        
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

        [HttpPost]
        public IActionResult Login()
        {
            return CreatedAtAction(nameof(GetById), null);
        }

    }
}
