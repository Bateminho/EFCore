using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApp.Models
{
    public class Customer
    {
        public ObjectId Id { get; set; }
        [BsonElement("customerAuId")]
        public string CustomerAuId { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("rating")]
        public int Rating { get; set; }
        [BsonElement("ratings")]
        public ICollection<Rating> Ratings { get; set; }
        [BsonElement("reservations")]
        public ICollection<Reservation> Reservations { get; set; }
    }

}
