using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubmissionService.Application.DTOs;
using SubmissionService.Application.Features.Coordinator.Commands;
using SubmissionService.Application.Features.Coordinator.Queries;
using IMediator = MediatR.IMediator;

namespace SubmissionService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AnyAllowedUser")]

    public class CoordinatorController : ControllerBase
    {

        private readonly IMediator _mediator;
        public CoordinatorController(
            ILogger<CoordinatorController> logger, IMediator mediator

            )
        {

            _mediator = mediator;
        }
        [HttpGet]
        

        [HttpGet]
        public async Task<ActionResult<List<RecipientDto>>> GetAll()
        {
            var mileStones = await _mediator.Send(new GetAllCoordinatorQuery());
            return Ok(mileStones);
        }

        [HttpPost]
        public async Task<ActionResult<RecipientDto>> Create([FromBody] CreateCoordinatorCommand command)
        {

            var createdMileStone = await _mediator.Send(command);
            return Ok(createdMileStone);
        }


    }



}
