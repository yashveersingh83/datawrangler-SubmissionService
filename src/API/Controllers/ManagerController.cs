using MassTransit;
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
        private readonly IPublishEndpoint _publishEndpoint;
        public ManagerController(
            ILogger<ManagerController> logger, IMediator mediator
, IPublishEndpoint publishEndpoint
            )
        {

            _mediator = mediator;
            _publishEndpoint = publishEndpoint;
        }

        [Authorize(Policy = "AnalystOnly")]
        [HttpGet]
        public async Task<ActionResult<List<OrganizationalUnitHeadDto>>> GetAll()
        {
            var mileStones = await _mediator.Send(new GetAllManagerQuery());

            await _publishEndpoint.Publish(new MileStoneDto { Comments="Sample Message"});
            return Ok(mileStones);
        }

        
        [Authorize(Policy = "AnalystOnly")]
        [HttpGet("GetOrgUnits")]
        public async Task<ActionResult<List<OrganizationalUnitDto>>> GetOrgUnits()
        {
            var organizationalUnits = await _mediator.Send(new GetAllOrgUnitQuery());
            return Ok(organizationalUnits);
        }

        [HttpPost]
        public async Task<ActionResult<OrganizationalUnitHeadDto>> Create([FromBody] CreateManagerCommand command)
        {

            var createdMileStone = await _mediator.Send(command);
            return Ok(createdMileStone);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<OrganizationalUnitHeadDto>> Update(Guid id, [FromBody] UpdateManagerCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID in the URL does not match ID in the body.");
            }

            var updatedManager = await _mediator.Send(command);
            return Ok(updatedManager);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
             await _mediator.Send(new DeleteManagerCommand(id));
             return NoContent();
        }


    }



}
