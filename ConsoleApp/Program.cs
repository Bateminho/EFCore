using ConsoleApp;
using ConsoleApp.db;
using ConsoleApp.db.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Cryptography;
using ConsoleApp.Migrations;
using System;

using (var db = new MyDbContext())
{
	
	ClearData(db);
//	SeedDb(db);

//	// 1. Query

//	var canteenName = "Kgl. Bibliotek";

//	var menuOptions = from meal in db.Meals
//					  join canteen in db.Canteens on meal.CanteenId equals canteen.CanteenId
//					  where canteen.CanteenName == canteenName
//					  select new { meal.MealName, meal.Type };
//	Console.Write("\n ------------------------------------------");
//	Console.Write($"\n\nInput to used for query: {canteenName}");
//	Console.WriteLine("\nThe day's menu options for {0} are:", canteenName);
//	foreach (var option in menuOptions)
//	{
//		Console.WriteLine("Meal Name: {0}, Meal Type: {1}", option.MealName, option.Type);
//	}

//	// 2. Query

//	var customerCpr = "010120-4006";
//	var reservationList = (from r in db.ReservationLists
//						   join re in db.Reservations on r.ReservationId equals re.ReservationId
//						   join m in db.Meals on r.MealId equals m.MealId
//						   join c in db.Canteens on m.CanteenId equals c.CanteenId
//						   join cus in db.Customers on re.CustomerId equals cus.CustomerId
//						   where cus.CustomerCPR == customerCpr
//						   select r)
//		.FirstOrDefault();

//	if (reservationList != null)
//	{
//		Console.Write("\n ------------------------------------------");
//		Console.Write($"\n\nInput to used for query: {customerCpr}");
//		Console.WriteLine($"\nMealId: {reservationList.MealId}, MealName: {reservationList.Meal.MealName}, CanteenName: {reservationList.Meal.Canteen.CanteenName}");
//	}
//	else
//	{
//		Console.WriteLine("No reservation found for the specified customer.");
//	}

//	// 3. Query

//	var canteenName2 = "Kgl. Bibliotek";

//	var reservations = from reservation in db.Reservations
//					   join meal in db.Meals on reservationList.MealId equals meal.MealId
//					   join canteen in db.Canteens on meal.CanteenId equals canteen.CanteenId
//					   where canteen.CanteenName == canteenName2
//					   group reservation by meal.MealName into g
//					   select new
//					   {
//						   Name = g.Key,
//						   Amount = g.Count()
//					   };

//	Console.Write("\n ------------------------------------------");
//	Console.Write($"\n\nInput used for query: {canteenName2}");
//	Console.WriteLine("\nThe number of reservations for each daily menu option at {0} are:", canteenName2);

//	foreach (var reservation in reservations)
//	{
//		Console.WriteLine("{0}\t{1}", reservation.Name, reservation.Amount);
//	}

//	// 4. Query
//	string canteenName3 = "Kgl. Bibliotek";

//	var jitMeals = db.Meals
//		.Where(m => m.Canteen.CanteenName == canteenName3 && m.Type == MealType.JustInTime && !m.ReservationLists.Any(resList => db.Reservations.Any(res => res.ReservationId == resList.ReservationId && res.Status == ReservationStatus.Cancelled)))
//		.Select(m => m.MealName);

//	var cancelledMeals = db.Meals
//		.Where(m => m.Canteen.CanteenName == canteenName3 && m.ReservationLists.Any(resList => db.Reservations.Any(res => res.ReservationId == resList.ReservationId && res.Status == ReservationStatus.Cancelled)))
//		.Select(m => m.MealName);

//	Console.Write("\n ------------------------------------------");
//	Console.Write($"\n\nInput used for query: {canteenName3}");
//	Console.WriteLine("\nFor {0}:", canteenName3);
//	Console.WriteLine($"Just-in-time meal options for {canteenName3}:");
//	foreach (var mealName in jitMeals)
//	{
//		Console.WriteLine(mealName);
//	}

//	Console.WriteLine($"\nCancelled meal options for {canteenName3}:");
//	foreach (var mealName in cancelledMeals)
//	{
//		Console.WriteLine(mealName);
//	}



//	// 5. Query
//	var inputCanteenName = "Kgl. Bibliotek"; // replace with actual input canteen name
//	var cancelledReservations = db.Reservations
//		.Where(r => r.Status == ReservationStatus.Cancelled)
//		.Include(r => r.Canteen)
//		.Include(r => r.ReservationLists).ThenInclude(rl => rl.Meal)
//		.ToList();
//	Console.Write("\n ------------------------------------------");
//	Console.WriteLine("\nCancelled Reservations:");
//	Console.WriteLine("Canteen Name\tAddress\tZip Code\tReservation Date\tMeal Name");

//	foreach (var reservation in cancelledReservations)
//	{
//		if (reservation.Canteen.CanteenName == inputCanteenName)
//		{
//			// exclude the input canteen and include the other two canteens
//			foreach (var otherCanteenReservation in cancelledReservations
//				         .Where(r => r.Canteen.CanteenName != inputCanteenName))
//			{
//				Console.WriteLine($"{otherCanteenReservation.Canteen.CanteenName}\t{otherCanteenReservation.Canteen.Address}\t{otherCanteenReservation.Canteen.ZipCode}\t{otherCanteenReservation.Reservationtime:d}\t\t{otherCanteenReservation.ReservationLists.FirstOrDefault()?.Meal.MealName}");
//			}
//			break; // exit the loop after showing the other canteens
//		}
//		else
//		{
//			Console.WriteLine($"{reservation.Canteen.CanteenName}\t{reservation.Canteen.Address}\t{reservation.Canteen.ZipCode}\t{reservation.Reservationtime:d}\t\t{reservation.ReservationLists.FirstOrDefault()?.Meal.MealName}");
//		}
//	}

//	// 6. Query
//	var canteenRatings = db.Canteens
//		.Select(c => new
//		{
//			Name = c.CanteenName,
//			AvgRating = c.AVGRating
//		})
//		.OrderByDescending(c => c.AvgRating);
//	Console.Write("\n ------------------------------------------");
//	Console.WriteLine("Canteen Ratings:");
//	Console.WriteLine("Name\t\tAvg Rating");

	
//}


//void SeedDb(MyDbContext db)
//{

//	// Create Canteens
//	if (!db.Canteens.Any())
//	{
		
//		var canteens = new Canteen[]
//		{
//			new Canteen { CanteenName = "Kgl. Bibliotek", Address = "Nygade 6, Aarhus C", ZipCode = 8000, AVGRating = 4.2f}, 
//			new Canteen { CanteenName = "Kemisk Canteen", Address = "Nørregade 10, Aarhus N", ZipCode = 8200 , AVGRating = 4.8f},
//			new Canteen { CanteenName = "Matematisk Canteen", Address = "Silkeborgvej 20, Aarhus V", ZipCode = 8210, AVGRating = 3.8f}
//		};

//		foreach (Canteen canteen in canteens)
//		{
//			if (db.Canteens.Any(c => c.CanteenName == canteen.CanteenName))
//			{
//				Console.WriteLine($"Canteen {canteen.CanteenName} already exists in the database.");
//				Console.WriteLine($"Existing canteen:");
//				Console.WriteLine(db.Canteens.Single(c => c.CanteenName == canteen.CanteenName));
//			}
//			else
//			{
				
//				db.Canteens.Add(canteen);
//			}
//		}

//		db.SaveChanges();
//	}

//	// Create Customers
//	if (!db.Customers.Any())
//	{
//		var customers = new List<Customer>
//		{
//			new Customer
//			{
//				CustomerCPR = "010120-4006", Name = "Josefine Jørgensen", Ratings = new List<Rating>{new Rating{RatingValue = 4, CanteenId = 0}},
//				Reservations = new List<Reservation>()
//			},
//			new Customer
//			{
//				CustomerCPR = "020200-2006", Name = "Mette Andersen", Ratings = new List<Rating>{new Rating{RatingValue = 4, CanteenId = 2}},
//				Reservations = new List<Reservation>()
//			},
//			new Customer
//			{
//				CustomerCPR = "030300-2007", Name = "Søren Nielsen", Ratings = new List<Rating>{new Rating{RatingValue = 3, CanteenId = 0}},
//				Reservations = new List<Reservation>()
//			},
//			new Customer
//			{
//				CustomerCPR = "040400-2008", Name = "Karen Christensen", Ratings = new List<Rating>{ new Rating { RatingValue = 4, CanteenId = 1 } },
//				Reservations = new List<Reservation>()
//			},
//			new Customer
//			{
//				CustomerCPR = "050500-2009", Name = "Peter Pedersen", Ratings = new List<Rating>{ new Rating { RatingValue = 3, CanteenId = 2 } },
//				Reservations = new List<Reservation>()
//			},
//			new Customer
//			{
//				CustomerCPR = "060600-2010", Name = "Marianne Jensen", Ratings = new List<Rating>{ new Rating { RatingValue = 5, CanteenId = 0 } },
//				Reservations = new List<Reservation>()
//			},
//			new Customer
//			{
//				CustomerCPR = "070700-2011", Name = "Jørgen Hansen", Ratings = new List<Rating>{ new Rating { RatingValue = 3, CanteenId = 0 } },
//				Reservations = new List<Reservation>()
//			},
//			new Customer
//			{
//				CustomerCPR = "080800-2012", Name = "Gitte Madsen", Ratings = new List<Rating>{ new Rating { RatingValue = 2, CanteenId = 1 } },
//				Reservations = new List<Reservation>()
//			},
//			new Customer
//			{
//				CustomerCPR = "090900-2013", Name = "Henrik Petersen", Ratings = new List<Rating>{ new Rating { RatingValue = 4, CanteenId = 2 } },
//				Reservations = new List<Reservation>()
//			},
//		};

//		db.Customers.AddRange(customers);
//		db.SaveChanges();
//	}


//	if (!db.Meals.Any())
//	{
//		var canteens = db.Canteens.ToList();
//		var meals = new List<Meal>
//		{
//			new Meal { MealName = "Green Curry", Type = MealType.WarmDish, CanteenId = canteens[0].CanteenId },
//			new Meal { MealName = "Pizza", Type = MealType.StreetFood, CanteenId = canteens[0].CanteenId },
//			new Meal { MealName = "JIT-Sandwich", Type = MealType.JustInTime, CanteenId = canteens[0].CanteenId },
//			new Meal { MealName = "JIT-Soup", Type = MealType.JustInTime, CanteenId = canteens[0].CanteenId },
//			new Meal { MealName = "Tacos", Type = MealType.WarmDish, CanteenId = canteens[2].CanteenId },
//			new Meal { MealName = "Pizza", Type = MealType.StreetFood, CanteenId = canteens[0].CanteenId },
//			new Meal { MealName = "Red Curry", Type = MealType.WarmDish, CanteenId = canteens[1].CanteenId },
//			new Meal { MealName = "Pizza", Type = MealType.StreetFood, CanteenId = canteens[1].CanteenId },
//			new Meal { MealName = "Indian Curry", Type = MealType.WarmDish, CanteenId = canteens[2].CanteenId },
//			new Meal { MealName = "Burger", Type = MealType.StreetFood, CanteenId = canteens[2].CanteenId },
//		};

//		db.Meals.AddRange(meals);
//		db.SaveChanges();
//	}

	

//	// Create Ratings
//	if (!db.Ratings.Any())
//	{
//		var customers = db.Customers.ToList();
//		var canteens = db.Canteens.ToList();
//		var ratings = new List<Rating>
//		{
//			new Rating { RatingValue = 4, CustomerId = customers[0].CustomerId, CanteenId = canteens[0].CanteenId },
//			new Rating { RatingValue = 5, CustomerId = customers[0].CustomerId, CanteenId = canteens[2].CanteenId },
//			new Rating { RatingValue = 3, CustomerId = customers[1].CustomerId, CanteenId = canteens[0].CanteenId },
//			new Rating { RatingValue = 2, CustomerId = customers[2].CustomerId, CanteenId = canteens[2].CanteenId },
//			new Rating { RatingValue = 1, CustomerId = customers[2].CustomerId, CanteenId = canteens[2].CanteenId },
//			new Rating { RatingValue = 4, CustomerId = customers[3].CustomerId, CanteenId = canteens[1].CanteenId },
//			new Rating { RatingValue = 5, CustomerId = customers[4].CustomerId, CanteenId = canteens[1].CanteenId },
//			new Rating { RatingValue = 2, CustomerId = customers[5].CustomerId, CanteenId = canteens[0].CanteenId },
//			new Rating { RatingValue = 3, CustomerId = customers[6].CustomerId, CanteenId = canteens[2].CanteenId },
//			new Rating { RatingValue = 4, CustomerId = customers[7].CustomerId, CanteenId = canteens[2].CanteenId },
//			new Rating { RatingValue = 1, CustomerId = customers[8].CustomerId, CanteenId = canteens[0].CanteenId }
//		};
//		db.Ratings.AddRange(ratings);
//		db.SaveChanges();

//		// Calculate average rating for each canteen and update AVGRating property
//		foreach (var canteen in canteens)
//		{
//			var canteenRatings = db.Ratings.Where(r => r.CanteenId == canteen.CanteenId).ToList();
//			if (canteenRatings.Any())
//			{
//				var avgRating = canteenRatings.Average(r => r.RatingValue);
//				canteen.AVGRating = avgRating;
//			}
//		}

//		db.SaveChanges();

//	}

//	if (!db.Reservations.Any())
//	{
//		var customers = db.Customers.ToList();
//		var canteens = db.Canteens.ToList();

//		var reservation1 = new Reservation
//		{
//			CustomerId = customers[0].CustomerId,
//			CanteenId = canteens[0].CanteenId,
//			Reservationtime = new DateTime(2023, 4, 18, 12, 0, 0),
//			Status = ReservationStatus.Reserved
//		};

//		var reservation2 = new Reservation
//		{
//			CustomerId = customers[1].CustomerId,
//			CanteenId = canteens[1].CanteenId,
//			Reservationtime = new DateTime(2023, 4, 19, 13, 30, 0),
//			Status = ReservationStatus.Cancelled
//		};
//		var reservation3 = new Reservation
//		{
//			CustomerId = customers[2].CustomerId,
//			CanteenId = canteens[2].CanteenId,
//			Reservationtime = new DateTime(2023, 4, 18, 12, 0, 0),
//			Status = ReservationStatus.Reserved
//		};

//		var reservation4 = new Reservation
//		{
//			CustomerId = customers[3].CustomerId,
//			CanteenId = canteens[2].CanteenId,
//			Reservationtime = new DateTime(2023, 4, 19, 13, 30, 0),
//			Status = ReservationStatus.Cancelled
//		};
//		var reservation5 = new Reservation
//		{
//			CustomerId = customers[4].CustomerId,
//			CanteenId = canteens[0].CanteenId,
//			Reservationtime = new DateTime(2023, 4, 18, 12, 0, 0),
//			Status = ReservationStatus.Reserved
//		};

//		var reservation6 = new Reservation
//		{
//			CustomerId = customers[5].CustomerId,
//			CanteenId = canteens[1].CanteenId,
//			Reservationtime = new DateTime(2023, 4, 19, 13, 30, 0),
//			Status = ReservationStatus.Cancelled
//		};

//		db.Reservations.AddRange(reservation1, reservation2, reservation3, reservation4, reservation5, reservation6);
//		db.SaveChanges();
//	}



//	// Create ReservationLists
//	if (!db.ReservationLists.Any())
//	{
//		try
//		{
//			var reservations = db.Reservations.ToList();
//			var meals = db.Meals.ToList();

//			var reservationLists = new List<ReservationList>
//			{
//				new ReservationList { ReservationId = reservations[0].ReservationId, MealId = meals[0].MealId },
//				new ReservationList { ReservationId = reservations[1].ReservationId, MealId = meals[1].MealId },
//				new ReservationList { ReservationId = reservations[2].ReservationId, MealId = meals[2].MealId },
//				new ReservationList { ReservationId = reservations[3].ReservationId, MealId = meals[3].MealId },
//				new ReservationList { ReservationId = reservations[4].ReservationId, MealId = meals[4].MealId },
//				new ReservationList { ReservationId = reservations[5].ReservationId, MealId = meals[5].MealId},
//			};

//			db.ReservationLists.AddRange(reservationLists);
//			db.SaveChanges();
//		}
//		catch (Exception ex)
//		{
//			Console.WriteLine("Exception caught: " + ex.Message);
//		}
//	}





}

void ClearData(MyDbContext db)
{
	var ratings = db.Ratings.ToList();
	var reservations = db.Reservations.ToList();
	var meals = db.Meals.ToList();
	var reservationLists = db.ReservationLists.ToList();
	var customers = db.Customers.ToList();
	var canteens = db.Canteens.ToList();
	
	
	db.RemoveRange(db.Ratings);
	db.RemoveRange(db.Reservations);
	db.RemoveRange(db.Meals);
	db.RemoveRange(db.ReservationLists);
	db.RemoveRange(db.Customers);
	db.RemoveRange(db.Canteens);
	

	db.SaveChanges();
}

