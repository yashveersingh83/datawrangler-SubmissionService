using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubmissionService.Application.DTOs;
using SubmissionService.Application.Features.Coordinator.Commands;
using SubmissionService.Application.Features.Coordinator.Queries;
using SubmissionService.Application.Features.InformationRequest.Commands;
using SubmissionService.Application.Features.InformationRequest.Queries;
using IMediator = MediatR.IMediator;

namespace SubmissionService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InformationRequestController : ControllerBase
    {

        private readonly IMediator _mediator;
        public InformationRequestController(
            ILogger<InformationRequestController> logger, IMediator mediator

            )
        {

            _mediator = mediator;
        }
        [HttpGet]
        [Authorize(Policy = "AnalystOnly")]
        [HttpGet]
        public async Task<ActionResult<List<InformationRequestDto>>> GetAll()
        {
            var mileStones = await _mediator.Send(new GetAllInformationRequestQuery());
            return Ok(mileStones);
        }

        [HttpPost]
        public async Task<ActionResult<InformationRequestDto>> Create([FromBody] CreateInformationRequestCommand command)
        {

            var createdMileStone = await _mediator.Send(command);
            return Ok(createdMileStone);
        }


    }



}
