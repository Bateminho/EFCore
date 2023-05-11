using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ConsoleApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ConsoleApp.Repo
{
    public class ReservationRepository : Repository<Reservation>
    {
        public bool CreateReservation(Reservation reservation)
        {
            return Insert(reservation);
        }

        public Reservation? GetReservationById(ObjectId id)
        {
            return Collection.Find(r => r.Id == id).FirstOrDefault();
        }

        public IList<Reservation> GetReservationsByCustomerId(ObjectId customerId)
        {
            return Collection.Find(r => r.CustomerId == customerId).ToList();
        }

        public IList<Reservation> GetReservationsByCanteenId(ObjectId canteenId)
        {
            return Collection.Find(r => r.CanteenId == canteenId).ToList();
        }

        public IQueryable<Reservation> GetAllCancelledReservations()
        {
            return Collection.AsQueryable().Where(r => r.Status == ReservationStatus.Cancelled);
        }


        public bool UpdateReservation(Reservation reservation)
        {
            var result = Collection.ReplaceOne(r => r.Id == reservation.Id, reservation);

            return result.ModifiedCount > 0;
        }

        public bool DeleteReservationById(ObjectId id)
        {
            var result = Collection.DeleteOne(r => r.Id == id);

            return result.DeletedCount > 0;
        }

        public bool DeleteReservation(Reservation reservation)
        {
            return DeleteReservationById(reservation.Id);
        }

        public void DeleteAll()
        {
            Collection.DeleteMany(_ => true);
        }

    }
}