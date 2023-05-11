using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Models;
using MongoDB.Driver;

namespace ConsoleApp.Repo
{
    public class Repository<TEntity> where TEntity : class
    {
        private const string CONNSTR = "mongodb://localhost:27017";
        private const string DB_NAME = "Madspild";

        internal readonly IMongoCollection<TEntity> Collection;

        public Repository()
        {
            var database = GetDatabase();
            Collection = GetCollection(database);
            Collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public bool Insert(TEntity entity)
        {
            try
            {
                Collection.InsertOne(entity);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection
                .Find(predicate)
                .ToList();
        }

        public IList<TEntity> GetAll()
        {
            return Collection
                .Find(_ => true)
                .ToList();
        }

        private IMongoDatabase GetDatabase()
        {
            var client = new MongoClient(CONNSTR);

            return client.GetDatabase(DB_NAME);
        }

        private IMongoCollection<TEntity> GetCollection(IMongoDatabase db)
        {
            return db.GetCollection<TEntity>(typeof(TEntity).Name);
        }

    }
}