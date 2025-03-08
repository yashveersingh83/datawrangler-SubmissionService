using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Driver;

namespace SubmissionService.Domain;

[Table("RequestStatus", Schema = "SANT")]
public class RequestStatus:IEntity
{

    //public Guid Id { get; set; }
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }

    public int IntId {  get; set; } 
  
    public string Status { get; set; }
}



