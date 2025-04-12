using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;
using SubmissionService.Application.Features.Cache.Query;
using SubmissionService.Domain;

namespace SubmissionService.Application.Features.InformationRequest.Queries
{
    public class GetAllInformationRequestQuery : IRequest<List<InformationRequestDto>> { }

    public class GetAllInformationRequestQueryHandler : IRequestHandler<GetAllInformationRequestQuery, List<InformationRequestDto>>
    {
        private readonly IRepository<Domain.InformationRequest> _repository;
        private readonly IMapper _mapper;
        private readonly IRedisCacheService _redisCacheService;
        private readonly IMediator _mediator;
        public GetAllInformationRequestQueryHandler(IRepository<Domain.InformationRequest> repository, IMapper mapper , IRedisCacheService redisCacheService, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            this._redisCacheService = redisCacheService;
            this._mediator = mediator;
        }

        public async Task<List<InformationRequestDto>> Handle(GetAllInformationRequestQuery request, CancellationToken cancellationToken)
        {
            var cacheWarmUpResult = await _mediator.Send(new CacheWarmUpQuery(), cancellationToken);
            if (!cacheWarmUpResult)
            {
                // Log the error or handle the failure gracefully
                Console.WriteLine("Cache warm-up failed. Proceeding with database fetch.");
            }


            var infoRequests = await _repository.GetAllAsync();
            var infoRequestDtos = _mapper.Map<List<InformationRequestDto>>(infoRequests);

            //Fetch Milestones from Redis Cache
            var milestones = await _redisCacheService.GetCacheAsync<List<MileStoneDto>>(CacheKeyConstant.MileStoneKey);
            var coordinators = await _redisCacheService.GetCacheAsync<List<RecipientDto>>(CacheKeyConstant.RecipientKey);
            var unitHeads = await _redisCacheService.GetCacheAsync<List<OrganizationalUnitHeadDto>>(CacheKeyConstant.ManagerCacheKey);
            var requestStatuses = await _redisCacheService.GetCacheAsync<List<RequestStatus>>(CacheKeyConstant.RequestStatusKey);
            var organizationalUnits = await _redisCacheService.GetCacheAsync<List<OrganizationalUnitDto>>(CacheKeyConstant.OrgUnitKey);

            // If Redis cache is empty, fetch from the database and repopulate the cache
            if (milestones == null || !milestones.Any() ||
                coordinators == null || !coordinators.Any() ||
                unitHeads == null || !unitHeads.Any())
            {
                // Log the cache miss and repopulate the cache
                Console.WriteLine("Cache miss detected. Repopulating cache...");
                await _mediator.Send(new CacheWarmUpQuery(), cancellationToken);

                // Fetch the data again from Redis
                milestones = await _redisCacheService.GetCacheAsync<List<MileStoneDto>>(CacheKeyConstant.MileStoneKey);
                coordinators = await _redisCacheService.GetCacheAsync<List<RecipientDto>>(CacheKeyConstant.RecipientKey);
                unitHeads = await _redisCacheService.GetCacheAsync<List<OrganizationalUnitHeadDto>>(CacheKeyConstant.ManagerCacheKey);
                organizationalUnits = await _redisCacheService.GetCacheAsync<List<OrganizationalUnitDto>>(CacheKeyConstant.OrgUnitKey);
                requestStatuses = await _redisCacheService.GetCacheAsync<List<RequestStatus>>(CacheKeyConstant.RequestStatusKey);
            }


            // If Redis cache is empty, return without milestone mapping


            //Map MilestoneID in InformationRequestDto to Milestone Date from Redis
            foreach (var infoRequest in infoRequestDtos)
            {
                var milestone = milestones.FirstOrDefault(m => m.Id == infoRequest.MilestoneID);
                var coordinator = unitHeads.FirstOrDefault(m => m.Id == infoRequest.RecipientID);
                var unithead = unitHeads.FirstOrDefault(m => m.Id == infoRequest.ApproverID);
                if (milestone != null)
                {
                    infoRequest.MileStoneDate = milestone.Targetdate;  // Set Milestone Date
                }
                if (coordinator != null)
                {
                    infoRequest.RecipientID = coordinator.Id;  
                    infoRequest.CoordinatorName = coordinator.FullName; 
                }
                if (unithead != null)
                {
                    infoRequest.ApproverID = unithead.Id;  
                    infoRequest.ApproverName = unithead.FullName;
                }
                if(organizationalUnits != null)
                {
                    var orgUnit = organizationalUnits.FirstOrDefault(m => m.Id == infoRequest.OrganizationalUnitID);
                    if (orgUnit != null)
                    {
                        infoRequest.OrganizationalUnitName = orgUnit.Division;  
                        infoRequest.OrganizationalUnitID = orgUnit.Id;
                    }
                }
                if (requestStatuses != null)
                {
                    var requestStatus = requestStatuses.FirstOrDefault(m => m.Id == infoRequest.RequestStatusID);
                    if (requestStatus != null)
                    {
                        infoRequest.RequestStatusID = requestStatus.Id;
                        infoRequest.RequestStatus = requestStatus.Status;
                    }
                }
            }

            return infoRequestDtos;
        }
    }
}
