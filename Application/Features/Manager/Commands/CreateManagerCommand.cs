using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;

namespace SubmissionService.Application.Features.Manager.Commands
{

    public class CreateManagerCommand : IRequest<OrganizationalUnitHeadDto>
    {
        public Guid Id { get; set; }

        public string DDSUCode
        {
            get; set;
        }

        public string Division
        {
            get; set;
        }

        public string DivisionCode
        {
            get; set;
        }

        public string SectionCode
        {
            get; set;
        }




        // return the full name based on the condition with DelegateFullName
        public string FullName
        {
            get;
            set;
        }

        public string FunctionalTitle
        {
            get; set;
        }


        // Represents if the person has approval role(SIR_APPROVERS)
        public bool ApproverRole
        {
            get; set;
        }

        // Represents if the person has contributors role(SIR_CONTRIBUTORS)
        public bool ContributorRole
        {
            get; set;
        }

        // comma separated values of the person who has access to more than one role assinged(SIR_CONTRIBUTORS) to home or other divisions
        public string CoordinatorRoles
        {
            get; set;
        }

        // checks if role has been delegated to someone then delegated personnel number should be used
        public string PersonnelNumber
        {
            get; set;
        }
        public string ManagerComment
        {
            get; set;
        }
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
                CoordinatorRoles = request.CoordinatorRoles,
                PersonnelNumber = request.PersonnelNumber,
                ManagerComment = request.ManagerComment,
            };

            await _repository.CreateAsync(organizationalUnitHead);

            return _mapper.Map<OrganizationalUnitHeadDto>(organizationalUnitHead);
        }
    }
}
