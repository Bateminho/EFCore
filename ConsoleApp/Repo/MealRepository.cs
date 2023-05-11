using ConsoleApp.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;

namespace ConsoleApp.Repo
{
    public class MealRepository : Repository<Meal>
    {
        public bool AddNewMeal(string mealName, MealType type, ObjectId canteenId)
        {
            Meal meal = new Meal { MealName = mealName, Type = type, CanteenId = canteenId };
            return Insert(meal);
        }

        public Meal FindMealById(ObjectId mealId)
        {
            return Collection.Find(m => m.Id == mealId).FirstOrDefault();
        }

        public IList<Meal> FindMealsByType(MealType type)
        {
            return Collection.Find(m => m.Type == type).ToList();
        }

        public IList<Meal> FindMealsByName(string name)
        {
            return Collection.Find(m => m.MealName == name).ToList();
        }

        public bool UpdateMeal(Meal meal)
        {
            ReplaceOneResult result = Collection.ReplaceOne(m => m.Id == meal.Id, meal);
            return result.ModifiedCount > 0;
        }

        public bool RemoveMealById(ObjectId mealId)
        {
            DeleteResult result = Collection.DeleteOne(m => m.Id == mealId);
            return result.DeletedCount > 0;
        }

        public bool RemoveMeal(Meal meal)
        {
            return RemoveMealById(meal.Id);
        }

        public IList<Meal> FindAll()
        {
            return Collection.Find(m => true).ToList();
        }

        public IList<Meal> FindForCanteen(ObjectId canteenId)
        {
            return Collection.Find(m => m.CanteenId == canteenId).ToList();
        }

        

        public void DeleteAll()
        {
            Collection.DeleteMany(_ => true);
        }

    }
}