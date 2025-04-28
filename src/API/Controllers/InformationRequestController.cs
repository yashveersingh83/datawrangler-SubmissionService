using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SubmissionService.Application.DTOs;
using SubmissionService.Application.Features.InformationRequest.Commands;
using SubmissionService.Application.Features.InformationRequest.Queries;
using SubmissionService.Domain;
using System.Linq.Expressions;
using IMediator = MediatR.IMediator;

namespace SubmissionService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "AnyAllowedUser")]
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
        
        [HttpGet]
        public async Task<ActionResult<List<InformationRequestDto>>> GetAll()
        {
            var mileStones = await _mediator.Send(new GetAllInformationRequestQuery());
            return Ok(mileStones);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<InformationRequestDto>> GetById(Guid id)
        {
            Expression<Func<InformationRequest, bool>> filter = m => m.Id == id;
            var informationRequest = await _mediator.Send(new GetInformationRequestByFilterQuery(filter));
            return Ok(informationRequest.First());
        }


        [HttpPost]
        public async Task<ActionResult<InformationRequestDto>> Create([FromBody] CreateInformationRequestCommand command)
        {

            var createdMileStone = await _mediator.Send(command);
            return Ok(createdMileStone);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<InformationRequestDto>> Update(Guid id, [FromBody] UpdateInformationRequestCommand command)
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
            await _mediator.Send(new DeleteInformationRequestCommand(id));
            return NoContent();
        }



    }



}
