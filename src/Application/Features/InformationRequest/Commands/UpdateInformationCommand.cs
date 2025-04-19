using AutoMapper;
using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SharedKernel;
using SubmissionService.Application.DTOs;

namespace SubmissionService.Application.Features.InformationRequest.Commands
{

    public class UpdateInformationRequestCommand : InformationRequestBase {
        public Guid Id { get; set; }
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

            inforequest.ApproverID = request.ApproverID;
            inforequest.ApproverName = request.ApproverName ;
            inforequest.InputWorksheetLink = request.InputWorksheetLink;
            inforequest.LatestSubmittedWorksheetLink = request.LatestSubmittedWorksheetLink;

            inforequest.RecipientID = request.RecipientID;
            inforequest.RequestStatusID = request.RequestStatusID;
            inforequest.MilestoneID = request.MilestoneID;
            inforequest.WorksheetDetails = request.WorksheetDetails;
            inforequest.StatusModifiedDate = request.StatusModifiedDate;
            inforequest.OrganizationalUnitID = request.OrganizationalUnitID;
            inforequest.SubmissionTypeID = Convert.ToInt32(request.SubmissionTypeID);

            inforequest.ModifiedDate= DateTime.UtcNow;
            

            await _repository.UpdateAsync(inforequest);

            return _mapper.Map<InformationRequestDto>(inforequest);
        }
    }
}
