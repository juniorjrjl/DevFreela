using AutoMapper;
using DevFreela.API.InputModel;
using DevFreela.API.ViewModel;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var entity = _userService.GetById(id);
            var viewModel = _mapper.Map<UserViewModel>(entity);
            return Ok(viewModel);
        }

        [HttpPost]
        public IActionResult Post([FromBody]NewUserInputModel inputModel)
        {
            var entity = _mapper.Map<User>(inputModel);
            if (inputModel.SkillsId is not null && inputModel.SkillsId.Any()){
                entity.UsersSkills = inputModel.SkillsId.Select(id => _mapper.Map<UserSkill>(id)).ToList();
            }
            var saved = _userService.Create(entity);
            var viewModel = _mapper.Map<SavedUserViewModel>(saved);
            return Created(nameof(GetById), viewModel);
        }

        [HttpPost("login")]
        public IActionResult Login()
        {
            return CreatedAtAction(nameof(GetById), null);
        }

    }
}
