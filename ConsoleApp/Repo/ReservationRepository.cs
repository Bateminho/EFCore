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

        public void PrintCancelledReservationsFromOtherCanteens(string canteenName)
        {
            var canteenRepo = new CanteenRepository();
            var canteen = canteenRepo.Find(c => c.CanteenName == canteenName).FirstOrDefault();

            if (canteen != null)
            {
                var mealRepo = new MealRepository();
                var cancelledReservations = Collection
                    .Find(r => r.ReservationStatus == "Cancelled" && r.CanteenId != canteen.Id)
                    .ToList();

                foreach (var reservation in cancelledReservations)
                {
                    var canteenWhereCancelled = canteenRepo.Find(c => c.Id == reservation.CanteenId).FirstOrDefault();
                    if (canteenWhereCancelled != null)
                    {
                        Console.WriteLine($"Canteen Name: {canteenWhereCancelled.CanteenName}");
                        foreach (var reservationList in reservation.ReservationLists)
                        {
                            var meal = mealRepo.Find(m => m.Id == reservationList.MealId).FirstOrDefault();
                            if (meal != null)
                            {
                                Console.WriteLine($"Meal Name: {meal.MealName}");
                            }
                        }
                        Console.WriteLine($"Zipcode: {canteenWhereCancelled.ZipCode}");
                    }
                }
            }
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