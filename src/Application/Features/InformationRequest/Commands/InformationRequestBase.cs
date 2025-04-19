using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SubmissionService.Application.DTOs;

namespace SubmissionService.Application.Features.InformationRequest.Commands
{
    public partial class InformationRequestBase : IRequest<InformationRequestDto>
    {



        public int SIRYear { get; set; } = 0;


        public string RequestNumber { get; set; }=string.Empty;


        public string DDSUCode { get; set; } = string.Empty;


        public string OrganizationalUnitName { get; set; } =string.Empty;


        public string SubmissionType { get; set; } = string.Empty;
        public string SubmissionTypeID { get; set; }


        public string InformationSought { get; set; } = string.Empty;


        public string SPQComment { get; set; } = string.Empty;


        public DateTime? WorksheetAvailabilityDate { get; set; }=null;


        public string WorksheetType { get; set; } = string.Empty;


        public Guid ApproverID { get; set; }

        
        public string ApproverName { get; set; } = string.Empty;


        public string InputWorksheetLink { get; set; } = string.Empty;


        public string LatestSubmittedWorksheetLink { get; set; } = string.Empty;

        public Guid RequestStatusID { get; set; }

        //public virtual RequestStatus RequestStatus { get; set; }
        public Guid RecipientID { get; set; }


        public Guid MilestoneID { get; set; }

        public Guid OrganizationalUnitID { get; set; }


        public string WorksheetDetails { get; set; } = string.Empty;

        public DateTime? StatusModifiedDate { get; set; } = null;
    }
}
