using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel;
using SubmissionService.Application.Features.Coordinator.Queries;
using SubmissionService.Application.Features.Manager.Queries;
using SubmissionService.Application.Features.Milestone.Queries;
using SubmissionService.Domain;

namespace SubmissionService.Application.Features.Cache.Query
{
    public class CacheWarmUpQuery : IRequest<bool> { }
    public class CacheWarmUpQueryHandler : IRequestHandler<CacheWarmUpQuery, bool>
    {
        private readonly IMediator _mediator;
        private readonly IRedisCacheService _redisCacheService;
        private readonly string _milestoneCacheKey = CacheKeyConstant.MileStoneKey;
        private readonly string _recipientCacheKey = CacheKeyConstant.RecipientKey;
        private readonly string _requestStatusCacheKey = CacheKeyConstant.RequestStatusKey;
        private readonly string _managerCacheKey = CacheKeyConstant.ManagerCacheKey;
        private readonly string _submissionCacheKey = CacheKeyConstant.SubmissionTypeKey;
        private readonly ILogger<CacheWarmUpQueryHandler> _logger;
        private IRepository<OrganizationalUnit> _orgUnitRepo;
        private IRepository<RequestStatus> _requestStatus;

        public CacheWarmUpQueryHandler(IMediator mediator, IRedisCacheService redisCacheService , ILogger<CacheWarmUpQueryHandler> logger, IRepository<OrganizationalUnit> orgUnitRepo, IRepository<RequestStatus> requestStatus)
        {
            _mediator = mediator;
            _redisCacheService = redisCacheService;
            _logger = logger;
            _orgUnitRepo = orgUnitRepo;
            _requestStatus = requestStatus;
        }

        public async Task<bool> Handle(CacheWarmUpQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Cache Warm-Up Started");
                //Fetch All Milestones
                var milestones = await _mediator.Send(new GetAllMileStonesQuery(), cancellationToken);
                if (milestones.Any())
                {
                    await _redisCacheService.SetCacheAsync(_milestoneCacheKey, milestones);
                }

                //Fetch All Recipients
                var recipients = await _mediator.Send(new GetAllCoordinatorQuery(), cancellationToken);
                if (recipients.Any())
                {
                    await _redisCacheService.SetCacheAsync(_recipientCacheKey, recipients);
                }

                var managerss = await _mediator.Send(new GetAllManagerQuery(), cancellationToken);
                if (managerss.Any())
                {
                    await _redisCacheService.SetCacheAsync(_managerCacheKey, managerss);
                }

                var submissionType = new List<SubmissionType>() {
                    new SubmissionType {Id=1 , Type="Text" } ,
                    new SubmissionType { Id=2 , Type="DB" },
                     new SubmissionType { Id=3 , Type="Excel" }
                };               


                if (submissionType.Any())
                {
                    await _redisCacheService.SetCacheAsync(_submissionCacheKey, submissionType);
                }

                var requestStatus = await _requestStatus.GetAllAsync();
                var orgUnit = await _orgUnitRepo.GetAllAsync();
                if (requestStatus.Any())
                {
                    await _redisCacheService.SetCacheAsync(CacheKeyConstant.RequestStatusKey, requestStatus);
                }
                if (orgUnit.Any())
                {
                    await _redisCacheService.SetCacheAsync(CacheKeyConstant.OrgUnitKey, orgUnit);
                }

                _logger.LogInformation("Cache Warm-Up Completed");
                return true;  
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cache Warm-Up Failed: {ex.Message}");
                return false; 
            }
        }
    }
}
