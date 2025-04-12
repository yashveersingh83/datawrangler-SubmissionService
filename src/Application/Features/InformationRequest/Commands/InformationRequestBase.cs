using MediatR;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SubmissionService.Application.DTOs;

namespace SubmissionService.Application.Features.InformationRequest.Commands
{
    public partial class InformationRequestBase : IRequest<InformationRequestDto>
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

        public Guid OrganizationalUnitID { get; set; }


        public string WorksheetDetails { get; set; }

        public DateTime? StatusModifiedDate { get; set; }
    }
}
