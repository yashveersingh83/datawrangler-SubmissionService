using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SharedKernel;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SubmissionService.Domain;

public class InformationRequest : HasLastModified,IEntity
{
    
    [BsonId]
    [BsonRepresentation(BsonType.String)]
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
    [BsonRepresentation(BsonType.String)]
    public Guid ApproverID { get; set; }

    [StringLength(100)]
    public string ApproverName { get; set; }

    [StringLength(500)]
    public string InputWorksheetLink { get; set; }

    [StringLength(500)]
    public string LatestSubmittedWorksheetLink { get; set; }

    [BsonRepresentation(BsonType.String)]
    public Guid RequestStatusID { get; set; }


    [BsonRepresentation(BsonType.String)]

    public Guid RecipientID { get; set; }


    [BsonRepresentation(BsonType.String)]


    public Guid MilestoneID { get; set; }
   
    

    [StringLength(1000)]
    public string WorksheetDetails { get; set; }

    public DateTime? StatusModifiedDate { get; set; }


}



