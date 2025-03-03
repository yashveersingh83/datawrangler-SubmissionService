using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;
using SubmissionService.Domain;

namespace SubmissionService.Application.Features.Coordinator.Commands
{

    public class CreateCoordinatorCommand : IRequest<RecipientDto>
    {
        public Guid Id { get; set; }
        public string Division
        {
            get; set;
        }
        public string Coordinator
        {
            get; set;
        }
        public string CoordinatorName
        {
            get; set;
        }
        public string DDSUCode
        {
            get; set;
        }
        public string OrganizationalUnitName
        {
            get; set;
        }
        public string Comments
        {
            get; set;
        }
        public string LastModifiedBy
        {
            get; set;
        }
        public DateTime LastModifiedDate
        {
            get; set;
        }
        public bool IsActive
        {
            get; set;
        }
        public string ActiveText => IsActive ? "Active" : string.Empty;
        public int? NumberOfPendingRequests
        {
            get; set;
        }
        public bool IsInRole
        {
            get; set;
        }

        // comma separated values of the person who has access to more than one role assinged(SIR_CONTRIBUTORS) to home or other divisions
        public string CoordinatorRoles
        {
            get; set;
        }
    }
    public class CreateCoordinatorCommandHandler : IRequestHandler<CreateCoordinatorCommand, RecipientDto>
    {
        private readonly IRepository<Recipient> _repository;
        private readonly IMapper _mapper;

        public CreateCoordinatorCommandHandler(IRepository<Recipient> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<RecipientDto> Handle(CreateCoordinatorCommand request, CancellationToken cancellationToken)
        {
            var coordinator = new Recipient();
            coordinator.Id = Guid.NewGuid();
            coordinator.DDSUCode = request.DDSUCode;
            coordinator.Coordinator = request.Coordinator;
            coordinator.CoordinatorName = request.CoordinatorName;
            coordinator.OrganizationalUnitName = request.OrganizationalUnitName;
            coordinator.Comments = request.Comments;
            coordinator.ModifiedDate= DateTime.UtcNow;
            //coordinator.ModifiedBy=WindowsIdentity.GetCurrent().Name;

            await _repository.CreateAsync(coordinator);

            return _mapper.Map<RecipientDto>(coordinator);
        }
    }
}
