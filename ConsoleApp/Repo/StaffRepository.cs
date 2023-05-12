using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ConsoleApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ConsoleApp.Repo
{
    public class StaffRepository : Repository<Staff>
    {
        public bool AddNewStaff(string name, string title, string salary, Canteen canteen)
        {
            Staff staff = new Staff { Name = name, Title = title, Salary = salary, CanteenId = canteen.Id };
            canteen.Staff.Add(staff); // Add the staff to the Canteen's list of staff
            var result = Insert(staff);
            if (!result) canteen.Staff.Remove(staff); // Remove the staff from the Canteen's list of staff if the insertion fails
            return result;
        }

        public Staff? FindStaffById(ObjectId id)
        {
            return Collection.Find(Builders<Staff>.Filter.Eq(staff => staff.Id, id)).FirstOrDefault();
        }

        public IList<Staff> FindStaffByName(string name)
        {
            return Collection.Find(Builders<Staff>.Filter.Eq(staff => staff.Name, name)).ToList();
        }

        public IList<Staff> FindStaffByTitle(string title)
        {
            return Collection.Find(Builders<Staff>.Filter.Eq(staff => staff.Title, title)).ToList();
        }

        public IList<Staff> FindStaffBySalary(string salary)
        {
            return Collection.Find(Builders<Staff>.Filter.Eq(staff => staff.Salary, salary)).ToList();
        }

        public bool RemoveStaffById(ObjectId id)
        {
            var result = Collection.DeleteOne(Builders<Staff>.Filter.Eq(staff => staff.Id, id));
            return result.DeletedCount > 0;
        }



        //public bool UpdateStaff(Staff staff)
        //{
        //    var filter = Builders<Staff>.Filter.Eq(s => s.Id, staff.Id);
        //    var update = Builders<Staff>.Update
        //        .Set(s => s.Name, staff.Name)
        //        .Set(s => s.Title, staff.Title)
        //        .Set(s => s.Salary, staff.Salary);
        //    var result = Collection.UpdateOne(filter, update);
        //    return result.ModifiedCount > 0;
        //}
        public void DeleteAll()
        {
            Collection.DeleteMany(_ => true);
        }

    }
}