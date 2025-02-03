using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SharedKernel;

namespace SubmissionService.API
{
    public class Item : IEntity
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTimeOffset CreatedDate { get; set; }
    }

}
