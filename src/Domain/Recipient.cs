using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SharedKernel;
using System.ComponentModel.DataAnnotations;

namespace SubmissionService.Domain;

public class Recipient : HasLastModified, IEntity
{  
    

    [StringLength(4)]
    
    public string DDSUCode { get; set; }

    [StringLength(100)]
    public string OrganizationalUnitName { get; set; }

    [StringLength(10)]
    
    public string Coordinator { get; set; }

    [StringLength(100)]
    public string CoordinatorName { get; set; }

    [StringLength(500)]
    public string Comments { get; set; }

    public bool IsActive { get; set; }
    public Guid Id { get;set; }
}



public class OrgUnit:IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string Division { get; set; }
    public string DDSUCode { get; set; }
    public string SectionCode { get; set; }
    public string Description { get; set; } // Description added
}