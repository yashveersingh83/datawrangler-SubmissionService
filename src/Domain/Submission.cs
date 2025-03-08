using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SubmissionService.Domain;

[Table("Submission", Schema = "SANT")]
public class Submission
{
    
    public int Id { get; set; }

    public Submission()
    {
        //InformationRequests = new List<InformationRequest>();
    }

    //[ForeignKey("InformationRequest")]
    //public int InformationrequestId { get; set; }


    public DateTime SubmissionDate { get; set; }

    public string Comments { get; set; }

    [StringLength(200)]
    public string SubmittedBy { get; set; }

    [StringLength(200)]
    public string SubmittedType { get; set; }
    [StringLength(200)]
    public string Status { get; set; }

    [NotMapped]
    public Recipient CurrentRecipient { get; set; }

    //public virtual ICollection<DocumentSubmitted> DocumentSubmitted { get; set; }
    //public virtual ICollection<InformationRequest> InformationRequests { get; set; }
}
