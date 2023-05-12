using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ConsoleApp.Models
{
    public class Reservation
    {
        public ObjectId Id { get; set; }
        [BsonElement("status")]
        public string ReservationStatus { get; set; }
        [BsonElement("reservationTime")]
        public DateTime ReservationTime { get; set; }
        [BsonElement("customerId")]
        public ObjectId CustomerId { get; set; }
        [BsonElement("canteenId")]
        public ObjectId CanteenId { get; set; }
        [BsonElement("reservationLists")]
        public ICollection<ReservationList> ReservationLists { get; set; }
    }

    
}