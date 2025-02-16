using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SubmissionService.Application;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;
using System.Security.Claims;

namespace SubmissionService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MilestoneController : ControllerBase
    {
        private readonly IMileStoneService mileStoneService;

        // private readonly IRepository<MileStoneDto> milestoneRepository;

        public MilestoneController(
            ILogger<MilestoneController> logger ,IMileStoneService mileStoneService
            
            
            //, IRepository<MileStone> milestoneRepository
            
            )
        {
            this.mileStoneService = mileStoneService;
            // this.milestoneRepository = milestoneRepository;
        }
        [HttpGet]
        //[Authorize(Policy = "AnalystOnly")]
        //[Authorize(Roles ="Analyst")]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<MileStoneDto>>> GetAsync()
        {
            var items = (await mileStoneService.GetMileStones())                        ;

            var claimsPrincipal = HttpContext.User.Claims;
            return Ok(items);
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MileStoneDto>> GetByIdAsync(Guid id)
        {
            var item = await mileStoneService.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST /items
        [HttpPost]
        public async Task<ActionResult<MileStoneDto>> PostAsync(MileStoneDto createItemDto)
        {
            var item = new MileStoneDto
            {
                Comments = createItemDto.Comments,
                Description = createItemDto.Description,
                Targetdate = createItemDto.Targetdate,
                SIRYear = createItemDto.SIRYear,
                IntId = createItemDto.IntId
            };

            await mileStoneService.CreateAsync(item);

            //await publishEndpoint.Publish(new CatalogItemCreated(item.Id, item.Name, item.Description));

            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.IntId }, item);
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, MileStoneDto updateItemDto)
        {
            var existingItem = await mileStoneService.GetAsync(m => m.Id == updateItemDto.Id);
            //var existingItem = new MileStoneDto();
            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Comments = updateItemDto.Comments;
            existingItem.Description = updateItemDto.Description;
            existingItem.Targetdate = updateItemDto.Targetdate;
            existingItem.SIRYear = updateItemDto.SIRYear;
            existingItem.IntId = updateItemDto.IntId;


            await mileStoneService.UpdateAsync(existingItem);

            // await publishEndpoint.Publish(new CatalogItemUpdated(existingItem.Id, existingItem.Name, existingItem.Description));

            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await mileStoneService.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

             mileStoneService.RemoveAsync(item.Id);

            //await publishEndpoint.Publish(new CatalogItemDeleted(id));

            return NoContent();
        }
    }
}
