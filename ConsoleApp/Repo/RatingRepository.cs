using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ConsoleApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ConsoleApp.Repo
{
    public class RatingRepository : Repository<Rating>
    {
        private readonly CanteenRepository _canteenRepository;

        public RatingRepository(CanteenRepository canteenRepository)
        {
            _canteenRepository = canteenRepository;
        }

        public new bool Insert(Rating entity)
        {
            var result = base.Insert(entity);

            if (result)
            {
                // Update the AVGRating property of the associated canteen
                var canteen = _canteenRepository.Collection
                    .Find(x => x.Id == entity.CanteenId)
                    .FirstOrDefault();

                if (canteen != null)
                {
                    canteen.AVGRating = canteen.GetAverageRating();
                    _canteenRepository.Collection.ReplaceOne(x => x.Id == canteen.Id, canteen);
                }
            }

            return result;
        }

        public bool AddNewRating(ObjectId customerId, float ratingValue, ObjectId canteenId, DateTime datetime)
        {
            Rating rating = new Rating { CustomerId = customerId, RatingValue = ratingValue, CanteenId = canteenId, Datetime = datetime };
            return Insert(rating);
        }




        public IDictionary<string, float> GetAverageRatingValuesForAllCanteens()
        {
            var averageRatingValues = new Dictionary<string, float>();

            var canteenRepo = new CanteenRepository();
            var canteens = canteenRepo.GetAll();

            foreach (var canteen in canteens)
            {
                var canteenId = canteen.Id;
                var ratings = Collection.Find(r => r.CanteenId == canteenId).ToList();
                var ratingValues = new List<float>();

                foreach (var rating in ratings)
                {
                    ratingValues.Add(rating.RatingValue);
                }

                var averageRating = ratingValues.Average();
                averageRatingValues[canteen.CanteenName] = averageRating;
            }

            return averageRatingValues;
        }






        public IList<Rating> FindRatingsByCustomerId(ObjectId customerId)
        {
            return Find(rating => rating.CustomerId == customerId);
        }

        public IList<Rating> FindRatingsByCanteenId(ObjectId canteenId)
        {
            return Find(rating => rating.CanteenId == canteenId);
        }

        public bool RemoveRatingById(ObjectId id)
        {
            return Collection.DeleteOne(Builders<Rating>.Filter.Eq(rating => rating.Id, id)).DeletedCount > 1;
        }

        public bool RemoveRating(Rating rating)
        {
            return Collection.DeleteOne(Builders<Rating>.Filter.Eq(r => r.Id, rating.Id)).DeletedCount > 1;
        }

        public void DeleteAll()
        {
            Collection.DeleteMany(_ => true);
        }

    }
}