using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;

namespace SubmissionService.Application.Features.Milestone.Commands
{


    public class UpdateMileStoneCommand : MileStoneCommandBase { }


    public class UpdateMileStoneCommandHandler : IRequestHandler<UpdateMileStoneCommand, MileStoneDto>
    {
        private readonly IRepository<MileStone> _repository;
        private readonly IMapper _mapper;
        private readonly IRedisCacheService _redisCacheService;
        public UpdateMileStoneCommandHandler(IRepository<MileStone> repository, IMapper mapper, IRedisCacheService redisCacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _redisCacheService = redisCacheService; 
        }

        public async Task<MileStoneDto> Handle(UpdateMileStoneCommand request, CancellationToken cancellationToken)
        {
            var existingMileStone = await _repository.GetAsync(request.Id);
            if (existingMileStone == null)
            {
                throw new KeyNotFoundException($"MileStone with ID {request.Id} not found.");
            }

            existingMileStone.Comments = request.Comments;
            existingMileStone.Targetdate = request.Targetdate;
            existingMileStone.IntId = request.IntId;
            existingMileStone.SIRYear = request.SIRYear;
            existingMileStone.Description = request.Description;

            await _repository.UpdateAsync(existingMileStone);
            await _redisCacheService.RemoveCacheAsync(CacheKeyConstant.MileStoneKey);
            return _mapper.Map<MileStoneDto>(existingMileStone);
        }
    }
}
