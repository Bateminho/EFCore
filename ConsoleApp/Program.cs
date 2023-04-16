using ConsoleApp;
using ConsoleApp.db;
using ConsoleApp.db.Entities;
using System.Linq;
using System.Security.Cryptography;

using (var db = new MyDbContext())
{
	SeedDb(db);

	//var canteenName = "Kgl. Bibliotek";

	//var menuOptions = from meal in db.Meals
	//				  join canteen in db.Canteens on meal.CanteenId equals canteen.CanteenId
	//				  where canteen.CanteenName == canteenName
	//				  select new { meal.MealName, meal.MealType };

	//Console.WriteLine("The day's menu options for {0} are:", canteenName);
	//foreach (var option in menuOptions)
	//{
	//	Console.WriteLine("Meal Name: {0}, Meal Type: {1}", option.MealName, option.MealType);
	//}

	//var customerCpr = "010120-4006";
	//var reservation = (from r in db.Reservations
	//		join m in db.Meals on r.MealId equals m.MealId
	//		join c in db.Canteens on m.CanteenId equals c.CanteenId
	//		where r.CustomerId == customerCpr
	//		select new { MealId = m.MealId, MealName = m.MealName, CanteenName = c.CanteenName })
	//	.FirstOrDefault();

	//if (reservation != null)
	//{
	//	Console.WriteLine("Meal ID: {0}\tName: {1}\tCanteen Name: {2}", reservation.MealId, reservation.MealName, reservation.CanteenName);
	//}
	//else
	//{
	//	Console.WriteLine("No reservation found for user {0}", customerCpr);
	//}

	//var canteenName = "Kgl. Bibliotek";

	//var reservations = from reservation in db.Reservations
	//	join meal in db.Meals on reservation.MealID equals meal.MealID
	//	join canteen in db.Canteens on meal.CanteenID equals canteen.CanteenID
	//	where canteen.CanteenName == canteenName
	//	group meal by meal.MealName into g
	//	select new
	//	{
	//		Name = g.Key,
	//		Amount = g.Count()
	//	};

	//Console.WriteLine("The number of reservations for each daily menu option at {0} are:", canteenName);
	//foreach (var reservation in reservations)
	//{
	//	Console.WriteLine("{0}: {1}", reservation.Name, reservation.Amount);
	//}

	//string canteenName = "Kgl. Bibliotek";

	//var jitMeals = from meal in db.Meals
	//	join canteen in db.Canteens on meal.CanteenID equals canteen.CanteenID
	//	where canteen.CanteenName == canteenName && (meal.MealType == "Just In Time") //|| meal.MealName == "JIT-Soup")
	//	select meal.MealName;


	//var availableDailyMenu = from meal in db.Meals
	//	join canteen in db.Canteens on meal.CanteenID equals canteen.CanteenID
	//	join reservation in db.Reservations on meal.MealID equals reservation.MealID into mealReservations
	//	where canteen.CanteenName == canteenName && meal.MealType != "Just In Time" && meal.MealType != "Warm Dish" && !mealReservations.Any()
	//	select meal.MealName;


	//Console.WriteLine("For {0}:", canteenName);
	//Console.WriteLine("Just-in-time meal options:");
	//foreach (string mealName in jitMeals)
	//{
	//	Console.WriteLine(mealName);
	//}

	//Console.WriteLine("\nAvailable daily menu:");
	//foreach (string mealName in availableDailyMenu)
	//{
	//	Console.WriteLine(mealName);
	//}









}


void SeedDb(MyDbContext db)
{
	// Create Meals
	//var meals = new Meal[]
	//	{
	//		new Meal { MealName = "Green Curry", MealType = "Warm Dish", CanteenId = 1 },
	//		new Meal { MealName = "Pizza", MealType = "Street Food", CanteenId = 1 },
	//		new Meal { MealName = "JIT-Sandwich", MealType = "Just In Time", CanteenId = 1 },
	//		new Meal { MealName = "JIT-Soup", MealType = "Just in Time", CanteenId = 1 },
	//		new Meal { MealName = "Green Curry", MealType = "Warm Dish", CanteenId = 3 },
	//		new Meal { MealName = "Pizza", MealType = "Street Food", CanteenId = 1 },
	//		new Meal { MealName = "Green Curry", MealType = "Warm Dish", CanteenId = 2 },
	//		new Meal { MealName = "Pizza", MealType = "Street Food", CanteenId = 2 },
	//		new Meal { MealName = "Green Curry", MealType = "Warm Dish", CanteenId = 3 },
	//		new Meal { MealName = "Pizza", MealType = "Street Food", CanteenId = 3 }
	//};
	
	//foreach (Meal meal in meals)
	//{
	//	db.Meals.Add(meal);
	//}
	//db.SaveChanges();
	
	//// Create Customers
	//var customers = new Customer[]
	//{
	//	new Customer { CustomerCPR = "010120-4006", Name = "Naruto Uzumaki", Rating = 2, MealId = meals[0].MealId },
	//	new Customer { CustomerCPR = "020200-2006", Name = "Sasuke Uchiha", Rating = 5, MealId = meals[1].MealId },
	//	new Customer { CustomerCPR = "030300-2007", Name = "Sakura Haruno", Rating = 4, MealId = meals[2].MealId },
	//	new Customer { CustomerCPR = "040400-2008", Name = "Kakashi Hatake", Rating = 3, MealId = meals[3].MealId },
	//	new Customer { CustomerCPR = "050500-2009", Name = "Shikamaru Nara", Rating = 2, MealId = meals[4].MealId },
	//	new Customer { CustomerCPR = "060600-2010", Name = "Ino Yamanaka", Rating = 1, MealId = meals[5].MealId },
	//	new Customer { CustomerCPR = "070700-2011", Name = "Choji Akimichi", Rating = 3, MealId = meals[6].MealId },
	//	new Customer { CustomerCPR = "080800-2012", Name = "Hinata Hyuga", Rating = 4, MealId = meals[7].MealId },
	//	new Customer { CustomerCPR = "090900-2013", Name = "Neji Hyuga", Rating = 3, MealId = meals[8].MealId },
	//	new Customer { CustomerCPR = "101000-2014", Name = "Rock Lee", Rating = 2, MealId = meals[9].MealId },
	//};

	//foreach (Customer customer in customers)
	//{
	//	db.Customers.Add(customer);
	//}
	//db.SaveChanges();
	
	// Create Canteens

	var canteens = new Canteen[]
	{
		new Canteen { CanteenName = "Kgl. Bibliotek", Address = "Nygade 6, Aarhus ", ZipCode = 8000},
		new Canteen { CanteenName = "Kemisk Canteen", Address = "Nørregade 10, Aarhus", ZipCode = 8200},
		new Canteen { CanteenName = "Matematisk Canteen", Address = "Silkeborgvej 20, Aarhus", ZipCode = 8210}
	};

	foreach (Canteen canteen in canteens)
	{
		db.Canteens.Add(canteen);
	}

	db.SaveChanges();


	
	// Create Reservations
	//var reservations = new Reservation[]
	//{
	//	new Reservation
	//	{
	//		CustomerId = customers[0].CustomerId, CanteenId = canteens[0].CanteenId, CustomerId = meals[0].,
	//		Cancelled = false, Datetime = DateTime.Now
	//	},
	//	new Reservation
	//	{
	//		CustomerCPR = customers[1].CustomerCPR, CanteenID = canteens[1].CanteenID, MealID = meals[1].MealID,
	//		Cancelled = false, Datetime = DateTime.Now
	//	},
	//	new Reservation
	//	{
	//		CustomerCPR = customers[2].CustomerCPR, CanteenID = canteens[2].CanteenID, MealID = meals[2].MealID,
	//		Cancelled = false, Datetime = DateTime.Now
	//	},
	//	new Reservation
	//	{
	//		CustomerCPR = customers[3].CustomerCPR, CanteenID = canteens[0].CanteenID, MealID = meals[3].MealID,
	//		Cancelled = false, Datetime = DateTime.Now
	//	},
	//	new Reservation
	//	{
	//		CustomerCPR = customers[4].CustomerCPR, CanteenID = canteens[1].CanteenID, MealID = meals[4].MealID,
	//		Cancelled = false, Datetime = DateTime.Now
	//	},
	//	new Reservation
	//	{
	//		CustomerCPR = customers[5].CustomerCPR, CanteenID = canteens[2].CanteenID, MealID = meals[5].MealID,
	//		Cancelled = false, Datetime = DateTime.Now
	//	},
	//	new Reservation
	//	{
	//		CustomerCPR = customers[6].CustomerCPR, CanteenID = canteens[0].CanteenID, MealID = meals[6].MealID,
	//		Cancelled = true, Datetime = DateTime.Now
	//	},
	//	new Reservation
	//	{
	//		CustomerCPR = customers[7].CustomerCPR, CanteenID = canteens[1].CanteenID, MealID = meals[7].MealID,
	//		Cancelled = false, Datetime = DateTime.Now
	//	},
	//	new Reservation
	//	{
	//		CustomerCPR = customers[8].CustomerCPR, CanteenID = canteens[2].CanteenID, MealID = meals[8].MealID,
	//		Cancelled = false, Datetime = DateTime.Now
	//	},
	//	new Reservation
	//	{
	//		CustomerCPR = customers[9].CustomerCPR, CanteenID = canteens[0].CanteenID, MealID = meals[9].MealID,
	//		Cancelled = false, Datetime = DateTime.Now
	//	}
	//};


	//foreach (Reservation reservation in reservations)
	//{
	//	db.Reservations.Add(reservation);
	//}
	
	//db.SaveChanges();

	////Create Rating
	//var ratings = new Rating[]
	//{
	//	new Rating { CustomerId = customers[0].CustomerId, RatingValue = customers[0].Rating, CanteenId = canteens[0].CanteenId},
	//	new Rating { CustomerId = customers[1].CustomerId, RatingValue = customers[1].Rating, CanteenId = canteens[1].CanteenId},
	//	new Rating { CustomerId = customers[2].CustomerId , RatingValue = customers[2].Rating, CanteenId = canteens[2].CanteenId},
	//	new Rating { CustomerId = customers[3].CustomerId , RatingValue = customers[3].Rating, CanteenId = canteens[0].CanteenId},
	//	new Rating { CustomerId = customers[4].CustomerId , RatingValue = customers[4].Rating, CanteenId = canteens[1].CanteenId},
	//	new Rating { CustomerId = customers[5].CustomerId , RatingValue = customers[5].Rating, CanteenId = canteens[2].CanteenId},
	//	new Rating { CustomerId = customers[6].CustomerId , RatingValue = customers[6].Rating, CanteenId = canteens[0].CanteenId},
	//	new Rating { CustomerId = customers[7].CustomerId , RatingValue = customers[7].Rating, CanteenId = canteens[1].CanteenId},
	//	new Rating { CustomerId = customers[8].CustomerId , RatingValue = customers[8].Rating, CanteenId = canteens[2].CanteenId},
	//	new Rating { CustomerId = customers[9].CustomerId , RatingValue = customers[9].Rating, CanteenId = canteens[0].CanteenId}

	//};

	//foreach (Rating rating in ratings)
	//{
	//	db.Ratings.Add(rating);
	//}

	db.SaveChanges();

}

void ClearData(MyDbContext db)
{
	var meals = db.Meals.ToList();
	var customers = db.Customers.ToList();
	var canteens = db.Canteens.ToList();
	var reservations = db.Reservations.ToList();

	db.RemoveRange(db.Meals);
	db.RemoveRange(db.Customers);
	db.RemoveRange(db.Canteens);
	db.RemoveRange(db.Reservations);
	db.SaveChanges();
}



// Read
//Console.WriteLine("Querying for a blog");
//var blog = db.Blogs
//    .OrderBy(b => b.BlogId)
//    .First();

// Update
//Console.WriteLine("Updating the blog and adding a post");
//blog.Url = "https://devblogs.microsoft.com/dotnet";
//blog.Posts.Add(
//    new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
//db.SaveChanges();

// Delete
//Console.WriteLine("Delete the blog");
//db.Remove(blog);
//db.SaveChanges();