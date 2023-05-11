using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApp.Models
{
    public class Rating
    {
        public ObjectId Id { get; set; }
        [BsonElement("customerId")]
        public ObjectId CustomerId { get; set; }
        [BsonElement("ratingValue")]
        public float RatingValue { get; set; }
        [BsonElement("canteenId")]
        public ObjectId CanteenId { get; set; }
        [BsonElement("datetime")]
        public DateTime Datetime { get; set; }
    }
}