using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApp.Models
{
    public class ReservationList
    {
        public ObjectId Id { get; set; }
        [BsonElement("mealId")]
        public ObjectId MealId { get; set; }

        [BsonElement("reservationId")]
        public ObjectId ReservationId { get; set; }
    }
}