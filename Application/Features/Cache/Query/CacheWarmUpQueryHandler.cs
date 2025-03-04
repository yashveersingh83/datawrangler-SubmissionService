using MediatR;
using SharedKernel;
using SubmissionService.Application.Features.Coordinator.Queries;
using SubmissionService.Application.Features.Manager.Queries;
using SubmissionService.Application.Features.Milestone.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        

        public CacheWarmUpQueryHandler(IMediator mediator, IRedisCacheService redisCacheService)
        {
            _mediator = mediator;
            _redisCacheService = redisCacheService;
        }

        public async Task<bool> Handle(CacheWarmUpQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //Fetch All Milestones
                var milestones = await _mediator.Send(new GetAllMileStonesQuery(), cancellationToken);
                if ( milestones.Any())
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
