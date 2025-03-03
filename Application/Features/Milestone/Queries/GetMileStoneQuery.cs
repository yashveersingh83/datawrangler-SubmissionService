using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;

namespace SubmissionService.Application.Features.Milestone.Queries
{
    public class GetAllMileStonesQuery : IRequest<List<MileStoneDto>> { }

    public class GetAllMileStonesQueryHandler : IRequestHandler<GetAllMileStonesQuery, List<MileStoneDto>>
    {
        private  string key = "MileStone";
        private readonly IRepository<MileStone> _repository;
        private readonly IMapper _mapper;
        private IRedisCacheService _redisCacheService;

        public GetAllMileStonesQueryHandler(IRepository<MileStone> repository, IMapper mapper ,IRedisCacheService redisCacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _redisCacheService = redisCacheService;
        }

        public async Task<List<MileStoneDto>> Handle(GetAllMileStonesQuery request, CancellationToken cancellationToken)
        {
            var mileStones = await _repository.GetAllAsync();
            var data = _mapper.Map<List<MileStoneDto>>(mileStones);
            var cacheData = await _redisCacheService.GetCacheAsync<List<MileStoneDto>>(key);
            if (cacheData == null)
            {
                await _redisCacheService.SetCacheAsync<List<MileStoneDto>>(key, data);
            }
            else
            {
                return await _redisCacheService.GetCacheAsync<List<MileStoneDto>>(key);
            }

            return data;
        }
    }
}
