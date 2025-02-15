using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using SubmissionService.Domain;
using System.Security.Claims;

namespace SubmissionService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MilestoneController : ControllerBase
    {
        private readonly IRepository<MileStone> milestoneRepository;

        public MilestoneController(ILogger<MilestoneController> logger, IRepository<MileStone> milestoneRepository)
        {
            this.milestoneRepository = milestoneRepository;
        }
        [HttpGet]
        [Authorize(Policy = "AnalystOnly")]
        //[Authorize(Roles ="Analyst")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<MileStone>>> GetAsync()
        {
            var items = (await milestoneRepository.GetAllAsync())
                        ;

            var claimsPrincipal = HttpContext.User.Claims;
            return Ok(items);
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MileStone>> GetByIdAsync(Guid id)
        {
            var item = await milestoneRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST /items
        [HttpPost]
        public async Task<ActionResult<MileStone>> PostAsync(MileStone createItemDto)
        {
            var item = new MileStone
            {
                Comments = createItemDto.Comments,
                Description = createItemDto.Description,
                Targetdate = createItemDto.Targetdate,
                SIRYear = createItemDto.SIRYear,
                IntId=createItemDto.IntId
            };

            await milestoneRepository.CreateAsync(item);

            //await publishEndpoint.Publish(new CatalogItemCreated(item.Id, item.Name, item.Description));

            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.IntId }, item);
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, MileStone updateItemDto)
        {
            var existingItem = await milestoneRepository.GetAsync(m => m.IntId == updateItemDto.IntId);

            if (existingItem == null)
            {
                return NotFound();
            }

            existingItem.Comments = updateItemDto.Comments;
            existingItem.Description = updateItemDto.Description;
            existingItem.Targetdate = updateItemDto.Targetdate;
            existingItem.SIRYear = updateItemDto.SIRYear;
            existingItem.IntId = updateItemDto.IntId;
            

            await milestoneRepository.UpdateAsync(existingItem);

            // await publishEndpoint.Publish(new CatalogItemUpdated(existingItem.Id, existingItem.Name, existingItem.Description));

            return NoContent();
        }

        // DELETE /items/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var item = await milestoneRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            await milestoneRepository.RemoveAsync(item.Id);

            //await publishEndpoint.Publish(new CatalogItemDeleted(id));

            return NoContent();
        }
    }
}
