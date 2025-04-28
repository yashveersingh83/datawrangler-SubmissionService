using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using SubmissionService.Application;
using SubmissionService.Application.DTOs;
using SubmissionService.Application.Features.Milestone.Commands;
using SubmissionService.Application.Features.Milestone.Queries;
using SubmissionService.Domain;
using System.Linq.Expressions;
using IMediator = MediatR.IMediator;

namespace SubmissionService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AnyAllowedUser")]
    public class MilestoneController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly IRedisCacheService redisCacheService;

        public MilestoneController(
            ILogger<MilestoneController> logger, IMediator mediator,IRedisCacheService redisCacheService

            )
        {

            _mediator = mediator;
            this.redisCacheService = redisCacheService;
        }
        [HttpGet]
       
        [HttpGet]
        public async Task<ActionResult<List<MileStoneDto>>> GetAll()
        {
            var mileStones = await _mediator.Send(new GetAllMileStonesQuery());
            return Ok(mileStones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MileStoneDto>> GetById(Guid id)
        {
            var mileStone = await _mediator.Send(new GetMileStoneByIdQuery(id));
            return Ok(mileStone);
        }

        [HttpPost]
        public async Task<ActionResult<MileStoneDto>> Create([FromBody] CreateMileStoneCommand command)
        {
            var createdMileStone = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = createdMileStone.Id }, createdMileStone);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MileStoneDto>> Update(Guid id, [FromBody] UpdateMileStoneCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("ID in URL does not match ID in request body.");
            }

            var updatedMileStone = await _mediator.Send(command);
            return Ok(updatedMileStone);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteMileStoneCommand(id));
            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<ActionResult<List<MileStoneDto>>> GetByFilter([FromQuery] int IntId)
        {
            Expression<Func<MileStone, bool>> filter = m => m.IntId == IntId;
            var mileStones = await _mediator.Send(new GetMileStonesByFilterQuery(filter));
            return Ok(mileStones);
        }
    }



}
