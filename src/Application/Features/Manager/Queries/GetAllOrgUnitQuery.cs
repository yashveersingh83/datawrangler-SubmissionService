using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;

namespace SubmissionService.Application.Features.Manager.Queries
{
    public class GetAllOrgUnitQuery : IRequest<List<OrganizationalUnitDto>> { }

    public class GetAllOrgUnitQueryHandler : IRequestHandler<GetAllOrgUnitQuery, List<OrganizationalUnitDto>>
    {
        private readonly IRepository<OrganizationalUnit> _repository;
        private readonly IMapper _mapper;

        public GetAllOrgUnitQueryHandler(IRepository<OrganizationalUnit> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<OrganizationalUnitDto>> Handle(GetAllOrgUnitQuery request, CancellationToken cancellationToken)
        {
            var mileStones = await _repository.GetAllAsync();
            return _mapper.Map<List<OrganizationalUnitDto>>(mileStones);
        }
    }
}
