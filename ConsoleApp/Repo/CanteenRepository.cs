using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ConsoleApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ConsoleApp.Repo
{
    public class CanteenRepository : Repository<Canteen>
    {
        public IMongoCollection<Canteen> CanteenCollection => Collection;

        public bool NewCanteen(string name, int zipCode, string address, float avgRating)
        {
            // Check if a canteen with the same name already exists
            if (FindCanteenByName(name) != null)
            {
                // A canteen with the same name already exists, so don't insert a new one
                return false;
            }

            var canteen = new Canteen
            {
                CanteenName = name,
                ZipCode = zipCode,
                Address = address,
                AVGRating = avgRating,
                Staff = new List<Staff>(),
                Ratings = new List<Rating>(),
                Meals = new List<Meal>()
            };

            return Insert(canteen);
        }

        public Canteen? FindCanteenByName(string name)
        {
            return Find(c => c.CanteenName == name).FirstOrDefault();
        }

        public IList<Canteen> FindCanteensByZipCode(int low, int high)
        {
            return Find(c => c.ZipCode >= low && c.ZipCode <= high);
        }

        public IList<string> GetAllCanteenNames()
        {
            return Collection
                .Find(_ => true)
                .Project(c => c.CanteenName)
                .ToList();
        }

        

        public bool AddRatingToCanteen(ObjectId canteenId, float ratingValue)
        {
            var canteenRepo = new Repository<Canteen>();
            var ratingRepo = new Repository<Rating>();

            var canteen = canteenRepo.Find(x => x.Id == canteenId).FirstOrDefault();
            if (canteen == null)
            {
                return false;
            }

            var rating = new Rating
            {
                CanteenId = canteenId,
                RatingValue = ratingValue,
                Datetime = DateTime.Now
            };
            ratingRepo.Insert(rating);

            var ratings = ratingRepo.Find(x => x.CanteenId == canteenId);
            var averageRating = ratings.Average(x => x.RatingValue);

            canteen.AVGRating = averageRating;
            canteenRepo.Insert(canteen);

            return true;
        }


        private void Update(Canteen canteen)
        {
            throw new NotImplementedException();
        }

        public bool RemoveCanteenById(string id)
        {
            return Collection.DeleteOne(c => c.Id == ObjectId.Parse(id)).DeletedCount > 0;
        }

        public bool RemoveCanteen(Canteen canteen)
        {
            return Collection.DeleteOne(c => c.Id == canteen.Id).DeletedCount > 0;
        }

        public bool AddStaffToCanteen(string canteenId, Staff staff)
        {
            var filter = Builders<Canteen>.Filter.Eq(x => x.Id, ObjectId.Parse(canteenId));
            var update = Builders<Canteen>.Update.Push(x => x.Staff, staff);

            var result = Collection.UpdateOne(filter, update);

            return result.ModifiedCount > 0;
        }

        public bool RemoveStaffFromCanteen(string canteenId, Staff staff)
        {
            var filter = Builders<Canteen>.Filter.Eq(x => x.Id, ObjectId.Parse(canteenId));
            var update = Builders<Canteen>.Update.Pull(x => x.Staff, staff);

            var result = Collection.UpdateOne(filter, update);

            return result.ModifiedCount > 0;
        }

        public void PrintStaffInformation(string canteenName)
        {
            var canteen = FindCanteenByName(canteenName);

            if (canteen == null)
            {
                Console.WriteLine($"No canteen found with the name '{canteenName}'");
                return;
            }

            foreach (var staff in canteen.Staff)
            {
                Console.WriteLine($"Staff Id: {staff.Id}");
                Console.WriteLine($"Staff Name: {staff.Name}");
                Console.WriteLine($"Staff Title: {staff.Title}");
                Console.WriteLine($"Staff Salary: {staff.Salary}");
            }
        }



        public void DeleteAll()
        {
            Collection.DeleteMany(_ => true);
        }

    }
}
