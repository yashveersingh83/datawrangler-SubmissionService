using System.ComponentModel.DataAnnotations.Schema;

namespace SubmissionService.Domain;

[Table("RequestStatus", Schema = "SANT")]
public class RequestStatus
{
   
    public int ID { get; set; }

  
    public string Status { get; set; }
}



