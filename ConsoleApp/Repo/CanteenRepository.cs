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

        public IDictionary<string, float> GetCanteensAverageRatings()
        {
            var canteens = GetAll();
            var averageRatings = new Dictionary<string, float>();

            foreach (var canteen in canteens)
            {
                averageRatings.Add(canteen.CanteenName, canteen.GetAverageRating());
            }

            return averageRatings;
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

        public IDictionary<string, string> GetStaffPayrollByCanteenName(string canteenName)
        {
            var lookup = new BsonDocument("$lookup",
                new BsonDocument
                {
                    { "from", "Staff" },
                    { "localField", "Staff._id" },
                    { "foreignField", "_id" },
                    { "as", "Staff" }
                });

            var unwind = new BsonDocument("$unwind", "$Staff");

            var match = new BsonDocument("$match", new BsonDocument("CanteenName", canteenName));

            var project = new BsonDocument("$project",
                new BsonDocument
                {
                    { "_id", "$Staff._id" },
                    { "Name", "$Staff.Name" },
                    { "Title", "$Staff.Title" },
                    { "Salary", "$Staff.Salary" }
                });

            var pipeline = new[] { lookup, unwind, match, project };

            var result = Collection.Aggregate<BsonDocument>(pipeline).ToList();

            var payroll = new Dictionary<string, string>();

            foreach (var document in result)
            {
                payroll.Add(document["Name"].ToString(), document["Salary"].ToString());
            }

            return payroll;
        }


        public void DeleteAll()
        {
            Collection.DeleteMany(_ => true);
        }

    }
}
