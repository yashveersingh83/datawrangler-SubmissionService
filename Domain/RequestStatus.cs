using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SubmissionService.Domain;

[Table("RequestStatus", Schema = "SANT")]
public class RequestStatus
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int ID { get; set; }

    [StringLength(50)]
    public string Status { get; set; }
}



