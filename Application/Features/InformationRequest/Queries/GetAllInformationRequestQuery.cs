using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;

namespace SubmissionService.Application.Features.InformationRequest.Queries
{
    public class GetAllInformationRequestQuery : IRequest<List<InformationRequestDto>> { }

    public class GetAllInformationRequestQueryHandler : IRequestHandler<GetAllInformationRequestQuery, List<InformationRequestDto>>
    {
        private readonly IRepository<Domain.InformationRequest> _repository;
        private readonly IMapper _mapper;
        private readonly IRedisCacheService _redisCacheService;

        public GetAllInformationRequestQueryHandler(IRepository<Domain.InformationRequest> repository, IMapper mapper , IRedisCacheService redisCacheService)
        {
            _repository = repository;
            _mapper = mapper;
            this._redisCacheService = redisCacheService;
        }

        public async Task<List<InformationRequestDto>> Handle(GetAllInformationRequestQuery request, CancellationToken cancellationToken)
        {
            
            var infoRequests = await _repository.GetAllAsync();
            var infoRequestDtos = _mapper.Map<List<InformationRequestDto>>(infoRequests);

            //Fetch Milestones from Redis Cache
            var milestones = await _redisCacheService.GetCacheAsync<List<MileStoneDto>>(CacheKeyConstant.MileStoneKey);
            var coordinators = await _redisCacheService.GetCacheAsync<List<RecipientDto>>(CacheKeyConstant.RecipientKey);
            var unitHeads = await _redisCacheService.GetCacheAsync<List<OrganizationalUnitHeadDto>>(CacheKeyConstant.ManagerCacheKey);

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
            }

            return infoRequestDtos;
        }
    }
}
