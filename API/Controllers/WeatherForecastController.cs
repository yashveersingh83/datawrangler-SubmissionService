//using Microsoft.AspNetCore.Mvc;
//using SharedKernel;

//namespace SubmissionService.API.Controllers;

//[ApiController]
//[Route("[controller]")]
//public class WeatherForecastController : ControllerBase
//{
//    private readonly IRepository<Item> itemsRepository;
//    private static readonly string[] Summaries = new[]
//    {
//        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//    };

//    private readonly ILogger<WeatherForecastController> _logger;

//    public WeatherForecastController(ILogger<WeatherForecastController> logger, IRepository<Item> itemsRepository)
//    {
//        _logger = logger;
//        this.itemsRepository = itemsRepository;
//    }

//    [HttpGet]
//    public async Task<ActionResult<IEnumerable<Item>>> GetAsync()
//    {
//        var items = (await itemsRepository.GetAllAsync())
//                    ;

//        return Ok(items);
//    }

//    // GET /items/{id}
//    [HttpGet("{id}")]
//    public async Task<ActionResult<Item>> GetByIdAsync(Guid id)
//    {
//        var item = await itemsRepository.GetAsync(id);

//        if (item == null)
//        {
//            return NotFound();
//        }

//        return item;
//    }

//    // POST /items
//    [HttpPost]
//    public async Task<ActionResult<Item>> PostAsync(Item createItemDto)
//    {
//        var item = new Item
//        {
//            Name = createItemDto.Name,
//            Description = createItemDto.Description,
//            Price = createItemDto.Price,
//            CreatedDate = DateTimeOffset.UtcNow
//        };

//        await itemsRepository.CreateAsync(item);

//        //await publishEndpoint.Publish(new CatalogItemCreated(item.Id, item.Name, item.Description));

//        return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
//    }

//    // PUT /items/{id}
//    [HttpPut("{id}")]
//    public async Task<IActionResult> PutAsync(Guid id, Item updateItemDto)
//    {
//        var existingItem = await itemsRepository.GetAsync(id);

//        if (existingItem == null)
//        {
//            return NotFound();
//        }

//        existingItem.Name = updateItemDto.Name;
//        existingItem.Description = updateItemDto.Description;
//        existingItem.Price = updateItemDto.Price;

//        await itemsRepository.UpdateAsync(existingItem);

//        // await publishEndpoint.Publish(new CatalogItemUpdated(existingItem.Id, existingItem.Name, existingItem.Description));

//        return NoContent();
//    }

//    // DELETE /items/{id}
//    [HttpDelete("{id}")]
//    public async Task<IActionResult> DeleteAsync(Guid id)
//    {
//        var item = await itemsRepository.GetAsync(id);

//        if (item == null)
//        {
//            return NotFound();
//        }

//        await itemsRepository.RemoveAsync(item.Id);

//        //await publishEndpoint.Publish(new CatalogItemDeleted(id));

//        return NoContent();
//    }

//}