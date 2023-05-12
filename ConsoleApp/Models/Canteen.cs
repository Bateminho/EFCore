using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ConsoleApp.Models
{
    public class Canteen
    {
        public ObjectId Id { get; set; }
        [BsonElement("address")]
        public string Address { get; set; }
        [BsonElement("zipCode")]
        public int ZipCode { get; set; }
        [BsonElement("avgRating")]
        public float AVGRating { get; set; }
        [BsonElement("canteenName")]
        public string CanteenName { get; set; }

      
        // Navigation properties
        public ICollection<Staff> Staff { get; set; }

        [BsonElement("ratings")]
        public ICollection<Rating> Ratings { get; set; }
        [BsonElement("meals")]
        public ICollection<Meal> Meals { get; set; }

        public float GetAverageRating()
        {
            if (Ratings == null || Ratings.Count == 0)
            {
                return 0;
            }

            float sum = 0;
            foreach (var rating in Ratings)
            {
                sum += rating.RatingValue;
            }

            return sum / Ratings.Count;
        }
    }

}

