using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApp.Models
{
    public class Meal
    {
        public ObjectId Id { get; set; }
        [BsonElement("mealName")]
        public string MealName { get; set; }
        [BsonElement("type")]
        public string Type { get; set; }
        //public MealType Type { get; set; }
        [BsonElement("canteenId")]
        public ObjectId CanteenId { get; set; }
        [BsonElement("reservationLists")]
        public ICollection<ReservationList> ReservationLists { get; set; }
    }

    
}



