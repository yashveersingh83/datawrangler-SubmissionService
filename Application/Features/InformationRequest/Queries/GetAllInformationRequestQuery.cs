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

        public GetAllInformationRequestQueryHandler(IRepository<Domain.InformationRequest> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<InformationRequestDto>> Handle(GetAllInformationRequestQuery request, CancellationToken cancellationToken)
        {
            var mileStones = await _repository.GetAllAsync();
            return _mapper.Map<List<InformationRequestDto>>(mileStones);
        }
    }
}
