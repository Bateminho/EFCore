using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApp.Models
{
    public class Staff
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Salary { get; set; }

        // Navigation properties
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId CanteenId { get; set; }
        public Canteen Canteen { get; set; }

    }
}