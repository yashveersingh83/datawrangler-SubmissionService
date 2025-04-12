using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SharedKernel;

namespace SubmissionService.Domain;

public class OrganizationalUnit : IEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string Division { get; set; }
    public string DDSUCode { get; set; }
    public string SectionCode { get; set; }
    public string Description { get; set; } // Description added
}