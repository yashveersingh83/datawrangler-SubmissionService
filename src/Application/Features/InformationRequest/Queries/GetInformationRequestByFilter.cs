using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;
using System.Linq.Expressions;

namespace SubmissionService.Application.Features.InformationRequest.Queries
{
    public class GetInformationRequestByFilterQuery : IRequest<List<InformationRequestDto>> {
        public Expression<Func<SubmissionService.Domain.InformationRequest, bool>> Filter { get; }

        public GetInformationRequestByFilterQuery(Expression<Func<Domain.InformationRequest, bool>> filter)
        {
            Filter = filter;
        }
    }

    public class GetInformationRequestByFilterQueryHandler : IRequestHandler<GetInformationRequestByFilterQuery, List<InformationRequestDto>>
    {
        private readonly IRepository<Domain.InformationRequest> _repository;
        private readonly IMapper _mapper;

        public GetInformationRequestByFilterQueryHandler(IRepository<Domain.InformationRequest> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<InformationRequestDto>> Handle(GetInformationRequestByFilterQuery request, CancellationToken cancellationToken)
        {
            var informationRequests = await _repository.GetAllAsync(request.Filter);
            return _mapper.Map<List<InformationRequestDto>>(informationRequests);
        }
    }
}
