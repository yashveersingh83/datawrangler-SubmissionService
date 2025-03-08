using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubmissionService.Application.DTOs;
using SubmissionService.Application.Features.Manager.Commands;
using SubmissionService.Application.Features.Manager.Queries;
using IMediator = MediatR.IMediator;

namespace SubmissionService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ManagerController : ControllerBase
    {

        private readonly IMediator _mediator;
        public ManagerController(
            ILogger<ManagerController> logger, IMediator mediator

            )
        {

            _mediator = mediator;
        }
        [HttpGet]
        [Authorize(Policy = "AnalystOnly")]
        [HttpGet]
        public async Task<ActionResult<List<OrganizationalUnitHeadDto>>> GetAll()
        {
            var mileStones = await _mediator.Send(new GetAllManagerQuery());
            return Ok(mileStones);
        }

        [HttpPost]
        public async Task<ActionResult<OrganizationalUnitHeadDto>> Create([FromBody] CreateManagerCommand command)
        {

            var createdMileStone = await _mediator.Send(command);
            return Ok(createdMileStone);
        }


    }



}
