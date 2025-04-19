using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;

namespace SubmissionService.Application.Features.Manager.Commands
{

    public class CreateManagerCommand : CreateManagerCommandBase
    {
    }
    public class CreateManagerCommandHandler : IRequestHandler<CreateManagerCommand, OrganizationalUnitHeadDto>

    {
        private readonly IRepository<OrganizationalUnitHead> _repository;
        private readonly IMapper _mapper;

        public CreateManagerCommandHandler(IRepository<OrganizationalUnitHead> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<OrganizationalUnitHeadDto> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
        {
            var organizationalUnitHead = new OrganizationalUnitHead()
            {
                Id = Guid.NewGuid(),
                DDSUCode = request.DDSUCode,
                Division = request.Division,
                DivisionCode = request.DivisionCode,
                SectionCode = request.SectionCode,
                FullName = request.FullName,
                FunctionalTitle = request.FunctionalTitle,
                ApproverRole = true,
                ContributorRole = true,
               // CoordinatorRoles = request.CoordinatorRoles,
                PersonnelNumber = request.PersonnelNumber,
               // ManagerComment = request.ManagerComment,
            };

            await _repository.CreateAsync(organizationalUnitHead);

            return _mapper.Map<OrganizationalUnitHeadDto>(organizationalUnitHead);
        }
    }
}
