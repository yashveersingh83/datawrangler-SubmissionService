namespace SubmissionService.Application.DTOs;

public class InformationRequestDto
{
    public Guid Id { get; set; }
    //public int ID { get; set; }
    public int SIRYear { get; set; }
    public string InformationRequest { get; set; }
    public string RequestNumber { get; set; }
   
    public Guid RecipientID { get; set; }
    public string DDSUCode { get; set; }
    public string SubmissionType { get; set; }
    public string ExistingSubmissionType { get; set; }
    public string InformationSought { get; set; }
    public string SPQComment { get; set; }
    public DateTime? WorksheetAvailabilityDate { get; set; }
    public string WorksheetType { get; set; }
    public Guid MilestoneID { get; set; }
    public DateTime? MileStoneDate { get; set; }
 
    public Guid ApproverID { get; set; }
    public string ApproverName { get; set; }
    public string InputWorksheetLink { get; set; }
    public string LatestSubmittedWorksheetLink { get; set; }
    public string WorksheetDetails { get; set; }
    public string WorksheetTabs { get; set; }
    public Guid RequestStatusID { get; set; }
    
    public DateTime? LastModifiedDate { get; set; }
    public string OrganizationalUnitName { get; set; }
    public string RequestStatus { get; set; }
    public int RequestStatusType { get; set; }

    public string CoordinatorName { get; set; }
    

}
