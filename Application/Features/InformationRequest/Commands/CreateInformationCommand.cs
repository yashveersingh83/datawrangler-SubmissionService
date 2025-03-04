using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;

namespace SubmissionService.Application.Features.InformationRequest.Commands
{

    public class CreateInformationRequestCommand : IRequest<InformationRequestDto>
    {

        public Guid Id { get; set; }

        public int SIRYear { get; set; }

        
        public string RequestNumber { get; set; }

        
        public string DDSUCode { get; set; }

        
        public string OrganizationalUnitName { get; set; }

        
        public string SubmissionType { get; set; }

        
        public string InformationSought { get; set; }

        
        public string SPQComment { get; set; }

        
        public DateTime? WorksheetAvailabilityDate { get; set; }

        
        public string WorksheetType { get; set; }


        public Guid ApproverID { get; set; }


        public string ApproverName { get; set; }

        
        public string InputWorksheetLink { get; set; }

        
        public string LatestSubmittedWorksheetLink { get; set; }

        public Guid RequestStatusID { get; set; }

        //public virtual RequestStatus RequestStatus { get; set; }
        public Guid RecipientID { get; set; }


        public Guid MilestoneID { get; set; }


        
        public string WorksheetDetails { get; set; }

        public DateTime? StatusModifiedDate { get; set; }
    }
    public class CreateInformationRequestCommandHandler : IRequestHandler<CreateInformationRequestCommand, InformationRequestDto>
    {
        private readonly IRepository<Domain.InformationRequest> _repository;
        private readonly IMapper _mapper;

        public CreateInformationRequestCommandHandler(IRepository<Domain.InformationRequest> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<InformationRequestDto> Handle(CreateInformationRequestCommand request, CancellationToken cancellationToken)
        {
            var inforequest = new Domain.InformationRequest();
            inforequest.Id = Guid.NewGuid();
            inforequest.SIRYear = request.SIRYear;
            inforequest.RequestNumber = request.RequestNumber;
            inforequest.DDSUCode = request.DDSUCode;
            inforequest.OrganizationalUnitName = request.OrganizationalUnitName;
            inforequest.SubmissionType = request.SubmissionType;
            inforequest.InformationSought = request.InformationSought;
            inforequest.SPQComment = request.SPQComment;
            inforequest.WorksheetAvailabilityDate = request.WorksheetAvailabilityDate;
            inforequest.WorksheetType = request.WorksheetType;

            inforequest.ApproverID = request.ApproverID;
            inforequest.ApproverName = request.ApproverName;
            inforequest.InputWorksheetLink = request.InputWorksheetLink;
            inforequest.LatestSubmittedWorksheetLink = request.LatestSubmittedWorksheetLink;

            inforequest.RecipientID = request.RecipientID;
            inforequest.RequestStatusID = request.RequestStatusID;
            inforequest.MilestoneID = request.MilestoneID;
            inforequest.WorksheetDetails = request.WorksheetDetails;
            inforequest.StatusModifiedDate = request.StatusModifiedDate;
       
            inforequest.ModifiedDate= DateTime.UtcNow;
            

            await _repository.CreateAsync(inforequest);

            return _mapper.Map<InformationRequestDto>(inforequest);
        }
    }
}
