using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;

namespace SubmissionService.Application.Features.Manager.Queries
{
    public class GetAllManagerQuery : IRequest<List<OrganizationalUnitHeadDto>> { }

    public class GetAllManagerQueryHandler : IRequestHandler<GetAllManagerQuery, List<OrganizationalUnitHeadDto>>
    {
        private readonly IRepository<OrganizationalUnitHead> _repository;
        private readonly IMapper _mapper;

        public GetAllManagerQueryHandler(IRepository<OrganizationalUnitHead> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<OrganizationalUnitHeadDto>> Handle(GetAllManagerQuery request, CancellationToken cancellationToken)
        {
            var mileStones = await _repository.GetAllAsync();
            return _mapper.Map<List<OrganizationalUnitHeadDto>>(mileStones);
        }
    }
}
