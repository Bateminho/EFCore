using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ConsoleApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ConsoleApp.Repo
{
    public class ReservationListRepository : Repository<ReservationList>
    {
        public bool AddNewReservationList(ObjectId reservationId, ObjectId mealId)
        {
            ReservationList reservationList = new ReservationList { ReservationId = reservationId, MealId = mealId };
            return Insert(reservationList);
        }

        

        public IList<ReservationList> FindReservationListsByReservationId(ObjectId reservationId)
        {
            return Find(reservationList => reservationList.ReservationId == reservationId);
        }

        public IList<ReservationList> FindReservationListsByMealId(ObjectId mealId)
        {
            return Find(reservationList => reservationList.MealId == mealId);
        }

        public bool RemoveReservationListById(ObjectId id)
        {
            return Collection.DeleteOne(Builders<ReservationList>.Filter.Eq(reservationList => reservationList.ReservationId, id)).DeletedCount > 0;
        }

        public bool RemoveReservationList(ReservationList reservationList)
        {
            return RemoveReservationListById(reservationList.ReservationId);
        }



        public bool UpdateReservationList(ObjectId id, ReservationList reservationList)
        {
            var filter = Builders<ReservationList>.Filter.Eq(r => r.ReservationId, id);
            var update = Builders<ReservationList>.Update
                .Set(r => r.ReservationId, reservationList.ReservationId)
                .Set(r => r.MealId, reservationList.MealId);
            var result = Collection.UpdateOne(filter, update);
            return result.ModifiedCount > 0;
        }

        public void DeleteAll()
        {
            Collection.DeleteMany(_ => true);
        }

    }
}