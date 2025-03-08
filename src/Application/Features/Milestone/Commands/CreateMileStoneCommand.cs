using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;

namespace SubmissionService.Application.Features.Milestone.Commands
{
    public class CreateMileStoneCommand : MileStoneCommandBase { }


    public class CreateMileStoneCommandHandler : IRequestHandler<CreateMileStoneCommand, MileStoneDto>
    {
        private readonly IRepository<MileStone> _repository;
        private readonly IMapper _mapper;
        private readonly IRedisCacheService _redisCacheService;

        public CreateMileStoneCommandHandler(IRepository<MileStone> repository, IMapper mapper, IRedisCacheService redisCacheService)
        {
            _repository = repository;
            _mapper = mapper;
            this._redisCacheService = redisCacheService;
        }

        public async Task<MileStoneDto> Handle(CreateMileStoneCommand request, CancellationToken cancellationToken)
        {
            var mileStone = new    MileStone();
            mileStone.Comments = request.Comments;
            mileStone.Targetdate = request.Targetdate;
            mileStone.IntId = request.IntId;
            mileStone.SIRYear = request.SIRYear;
            mileStone.Description = request.Description;
            await _repository.CreateAsync(mileStone);

            await _redisCacheService.RemoveCacheAsync(CacheKeyConstant.MileStoneKey);

            return _mapper.Map<MileStoneDto>(mileStone);
        }

      
    }
}
