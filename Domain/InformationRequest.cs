using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SharedKernel;

namespace SubmissionService.Domain;

public class InformationRequest : HasLastModified,IEntity
{
    public InformationRequest()
    {

    }
   
    public Guid Id { get; set; }

    public int SIRYear { get; set; }

    [StringLength(50)]
    public string RequestNumber { get; set; }

    [StringLength(50)]
    public string DDSUCode { get; set; }

    [StringLength(100)]
    public string OrganizationalUnitName { get; set; }

    [StringLength(50)]
    public string SubmissionType { get; set; }

    [StringLength(4000)]
    public string InformationSought { get; set; }

    [StringLength(4000)]
    public string SPQComment { get; set; }

    [Column(TypeName = "Date")]
    public DateTime? WorksheetAvailabilityDate { get; set; }

    [StringLength(100)]
    public string WorksheetType { get; set; }

    [StringLength(10)]
    public string Approver { get; set; }

    [StringLength(100)]
    public string ApproverName { get; set; }

    [StringLength(500)]
    public string InputWorksheetLink { get; set; }

    [StringLength(500)]
    public string LatestSubmittedWorksheetLink { get; set; }

    public int RequestStatusID { get; set; }

   
    
    public virtual RequestStatus RequestStatus { get; set; }
    public int RecipientID { get; set; }

    
   
    

    public int MilestoneID { get; set; }
   
    

    [StringLength(1000)]
    public string WorksheetDetails { get; set; }

    public DateTime? StatusModifiedDate { get; set; }


}



