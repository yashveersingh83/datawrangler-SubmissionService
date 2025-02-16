using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;

namespace SubmissionService.Application.Features.Milestone.Commands
{
    public class CreateMileStoneCommand : IRequest<MileStoneDto>
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string? Comments { get; set; }
        public DateTime Targetdate { get; set; }
        public int IntId { get; set; }
        public int SIRYear { get; set; }
    }


    public class CreateMileStoneCommandHandler : IRequestHandler<CreateMileStoneCommand, MileStoneDto>
    {
        private readonly IRepository<MileStone> _repository;
        private readonly IMapper _mapper;

        public CreateMileStoneCommandHandler(IRepository<MileStone> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
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

            return _mapper.Map<MileStoneDto>(mileStone);
        }
    }
}
