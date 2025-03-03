using AutoMapper;
using MediatR;
using SharedKernel;
using SubmissionService.Application.DTOs;

namespace SubmissionService.Application.Features.InformationRequest.Commands
{

    public class UpdateInformationRequestCommand : IRequest<InformationRequestDto>
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

        
        public string Approver { get; set; }

        
        public string ApproverName { get; set; }

        
        public string InputWorksheetLink { get; set; }

        
        public string LatestSubmittedWorksheetLink { get; set; }

        public int RequestStatusID { get; set; }

        //public virtual RequestStatus RequestStatus { get; set; }
        public int RecipientID { get; set; }


        public int MilestoneID { get; set; }


        
        public string WorksheetDetails { get; set; }

        public DateTime? StatusModifiedDate { get; set; }
    }
    public class UpdateInformationRequestCommandHandler : IRequestHandler<UpdateInformationRequestCommand, InformationRequestDto>
    {
        private readonly IRepository<Domain.InformationRequest> _repository;
        private readonly IMapper _mapper;

        public UpdateInformationRequestCommandHandler(IRepository<Domain.InformationRequest> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<InformationRequestDto> Handle(UpdateInformationRequestCommand request, CancellationToken cancellationToken)
        {
            var inforequest = await _repository.GetAsync(request.Id);
            if (inforequest == null)
            {
                throw new KeyNotFoundException($"Information request  with ID {request.Id} not found.");
            }
           
            inforequest.SIRYear = request.SIRYear;
            inforequest.RequestNumber = request.RequestNumber;
            inforequest.DDSUCode = request.DDSUCode;
            inforequest.OrganizationalUnitName = request.OrganizationalUnitName;
            inforequest.SubmissionType = request.SubmissionType;
            inforequest.InformationSought = request.InformationSought;
            inforequest.SPQComment = request.SPQComment;
            inforequest.WorksheetAvailabilityDate = request.WorksheetAvailabilityDate;
            inforequest.WorksheetType = request.WorksheetType;

            inforequest.Approver = request.Approver;
            inforequest.ApproverName = request.ApproverName;
            inforequest.InputWorksheetLink = request.InputWorksheetLink;
            inforequest.LatestSubmittedWorksheetLink = request.LatestSubmittedWorksheetLink;

            inforequest.RecipientID = request.RecipientID;
            inforequest.RequestStatusID = request.RequestStatusID;
            inforequest.MilestoneID = request.MilestoneID;
            inforequest.WorksheetDetails = request.WorksheetDetails;
            inforequest.StatusModifiedDate = request.StatusModifiedDate;
       
            inforequest.ModifiedDate= DateTime.UtcNow;
            

            await _repository.UpdateAsync(inforequest);

            return _mapper.Map<InformationRequestDto>(inforequest);
        }
    }
}
