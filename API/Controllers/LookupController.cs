using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IMediator = MediatR.IMediator;

namespace SubmissionService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LookupController : ControllerBase
    {

        private readonly IMediator _mediator;
        public LookupController(
            ILogger<CoordinatorController> logger, IMediator mediator

            )
        {

            _mediator = mediator;
        }
       


    }



}
