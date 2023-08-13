using AutoMapper;
using DevFreela.API.ViewModel;
using DevFreela.Application.Queries.GetAllSkills;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    public class SkillsController : ControllerBase
    {


        public readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public SkillsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entity = await _mediator.Send(new GetAllSkillsQuery());
            var viewModel = _mapper.Map<SkillViewModel>(entity);
            return Ok(viewModel);
        }

    }
}