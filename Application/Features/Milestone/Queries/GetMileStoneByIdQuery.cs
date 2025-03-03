using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;

namespace SubmissionService.Application.Features.Milestone.Queries
{
    public class GetMileStoneByIdQuery : IRequest<MileStoneDto>
    {
        public Guid Id { get; set; }

        public GetMileStoneByIdQuery(Guid id)
        {
            Id = id;
        }
    }
    public class GetMileStoneByIdQueryHandler : IRequestHandler<GetMileStoneByIdQuery, MileStoneDto>
    {
        private readonly IRepository<MileStone> _repository;
        private readonly IMapper _mapper;

        public GetMileStoneByIdQueryHandler(IRepository<MileStone> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MileStoneDto> Handle(GetMileStoneByIdQuery request, CancellationToken cancellationToken)
        {
            var mileStone = await _repository.GetAsync(request.Id);
            return _mapper.Map<MileStoneDto>(mileStone);
        }
    }
}
