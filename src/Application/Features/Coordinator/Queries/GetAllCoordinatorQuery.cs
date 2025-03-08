using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;

namespace SubmissionService.Application.Features.Coordinator.Queries
{
    public class GetAllCoordinatorQuery : IRequest<List<RecipientDto>> { }

    public class GetAllCoordinatorQueryHandler : IRequestHandler<GetAllCoordinatorQuery, List<RecipientDto>>
    {
        private readonly IRepository<Recipient> _repository;
        private readonly IMapper _mapper;

        public GetAllCoordinatorQueryHandler(IRepository<Recipient> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<RecipientDto>> Handle(GetAllCoordinatorQuery request, CancellationToken cancellationToken)
        {
            var mileStones = await _repository.GetAllAsync();
            return _mapper.Map<List<RecipientDto>>(mileStones);
        }
    }
}
