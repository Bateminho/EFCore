using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ConsoleApp.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ConsoleApp.Repo
{
    public class CustomerRepository : Repository<Customer>
    {
        public bool AddNewCustomer(string customerAuId, string name, int rating)
        {
            Customer customer = new Customer
            {
                Id = ObjectId.GenerateNewId(),
                CustomerAuId = customerAuId,
                Name = name,
                Rating = rating
            };

            return Insert(customer);
        }

        public bool UpdateCustomerRating(string customerAuId, int newRating)
        {
            Customer customer = FindCustomerByAuId(customerAuId);
            if (customer != null)
            {
                customer.Rating = newRating;
                return Collection.ReplaceOne(Builders<Customer>.Filter.Eq(c => c.Id, customer.Id), customer).ModifiedCount > 0;
            }
            return false;
        }

        

        public Customer? FindCustomerByAuId(string customerAuId)
        {
            return Find(customer => customer.CustomerAuId == customerAuId).FirstOrDefault();
        }

        public IList<Customer> FindCustomersByRating(int ratingValue)
        {
            return Find(customer => customer.Rating == ratingValue);
        }

        public bool RemoveCustomerById(string id)
        {
            return Collection.DeleteOne(Builders<Customer>.Filter.Eq(customer => customer.Id, new ObjectId(id))).DeletedCount > 0;
        }

        public bool RemoveCustomer(Customer customer)
        {
            return Collection.DeleteOne(Builders<Customer>.Filter.Eq(c => c.Id, customer.Id)).DeletedCount > 0;
        }

        public void DeleteAll()
        {
            Collection.DeleteMany(_ => true);
        }

    }
}