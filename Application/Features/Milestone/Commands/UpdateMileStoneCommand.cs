using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;

namespace SubmissionService.Application.Features.Milestone.Commands
{
    public class UpdateMileStoneCommand : IRequest<MileStoneDto>
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string? Comments { get; set; }
        public DateTime Targetdate { get; set; }
        public int IntId { get; set; }
        public int SIRYear { get; set; }
    }


    public class UpdateMileStoneCommandHandler : IRequestHandler<UpdateMileStoneCommand, MileStoneDto>
    {
        private readonly IRepository<MileStone> _repository;
        private readonly IMapper _mapper;

        public UpdateMileStoneCommandHandler(IRepository<MileStone> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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
            return _mapper.Map<MileStoneDto>(existingMileStone);
        }
    }
}
