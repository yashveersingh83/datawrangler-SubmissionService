using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using SubmissionService.Application;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;
using IMediator = MediatR.IMediator;

namespace SubmissionService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LookupController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator _mediator;
        private readonly IRedisCacheService _redisCacheService;
        public LookupController(
            ILogger<CoordinatorController> logger,IMapper mapper , IMediator mediator,IRedisCacheService redisCacheService
        )
        {
            this.mapper = mapper;
            _mediator = mediator;
            _redisCacheService = redisCacheService;
        }

        [HttpGet("request-status")]
        public async Task<ActionResult<List<RequestStatus>>> GetRequestStatusCache()
        {
            var requestStatusCache = await _redisCacheService.GetCacheAsync<List<RequestStatus>>(CacheKeyConstant.RequestStatusKey);
            if (requestStatusCache == null)
            {
                return NotFound("RequestStatus cache is empty or not found.");
            }
            
            return Ok(requestStatusCache);
        }

        [HttpGet("submission-type")]
        public async Task<ActionResult<List<SubmissionTypeDto>>> GetSubmissionTypeCache()
        {
            var submissionTypeCache = await _redisCacheService.GetCacheAsync<List<Domain.SubmissionType>>(CacheKeyConstant.SubmissionTypeKey);
            if (submissionTypeCache == null)
            {
                return NotFound("SubmissionType cache is empty or not found.");
            }

            // Use AutoMapper to map SubmissionType to SubmissionTypeDto
            var submissionTypeDtoCache = mapper.Map<List<SubmissionTypeDto>>(submissionTypeCache);

            return Ok(submissionTypeDtoCache);
        }

    }



}
