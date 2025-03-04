using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SharedKernel;

namespace SubmissionService.Domain;

public class MileStone : IEntity
{    
    public string? Description { get; set; }
    public string? Comments { get; set; }
    public DateTime Targetdate { get; set; }
    public int IntId { get; set; }
    public int SIRYear { get; set; }

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    
}



