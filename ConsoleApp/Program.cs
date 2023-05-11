using System.Collections.ObjectModel;
using ConsoleApp.Models;
using ConsoleApp.Repo;
using DnsClient;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;

CanteenRepository canteens = new CanteenRepository();
CustomerRepository customers = new CustomerRepository();
MealRepository meals = new MealRepository();
ReservationRepository reservations = new ReservationRepository();
ReservationListRepository reservationLists = new ReservationListRepository();
RatingRepository ratings = new RatingRepository(canteens);
StaffRepository staffs = new StaffRepository();

//// Delete all collections if they exist
//if (canteens.GetAll().Any() || customers.GetAll().Any() || meals.GetAll().Any() ||
//    reservations.GetAll().Any() || reservationLists.GetAll().Any() || staffs.GetAll().Any())
{
    // Delete existing data
    canteens.DeleteAll();
    customers.DeleteAll();
    meals.DeleteAll();
    reservations.DeleteAll();
    reservationLists.DeleteAll();
    staffs.DeleteAll();
}

SeedDb();


////Query 1
//Console.WriteLine("\n\n--------------------------------------------");
//Console.WriteLine("Query 1: Find meals by canteen name\n");
//Canteen canteen = canteens.FindCanteenByName("Kgl. Bibliotek");
//List<Meal> mealsForCanteen = meals.FindForCanteen(canteen.Id)
//    .Select(meals => new Meal() { MealName = meals.MealName, Type = meals.Type })
//    .ToList();
//foreach (Meal meal in mealsForCanteen)
//{
//    Console.WriteLine($"Meal Name: {meal.MealName}, Type: {meal.Type}");
//}

////Query 2
//Console.WriteLine("\n\n--------------------------------------------");
//Console.WriteLine("Query 2: Find meals by canteen name\n");




//var customer = customers.FindCustomerByAuId("au030300");

//var reservationsForCustomer = reservations.GetReservationsByCustomerId(customer.Id).ToList();

//var mealsForCustomer = (from reservationList in reservationLists.GetAll()
//    join reservation in reservationsForCustomer on reservationList.ReservationId equals reservation.Id
//    join meal in meals.GetAll() on reservationList.MealId equals meal.Id
//    join canteen2 in canteens.GetAll() on meal.CanteenId equals canteen2.Id
//    select new { MealId = meal.Id, MealName = meal.MealName, CanteenName = canteen2.CanteenName }).ToList();

//foreach (var meal in mealsForCustomer)
//{
//    Console.WriteLine($"Meal ID: {meal.MealId} Name: {meal.MealName} Canteen: {meal.CanteenName}");
//}

//Console.WriteLine("\n\n--------------------------------------------");
//Console.WriteLine($"Query 3: Find meals by customerAuId ({customer.CustomerAuId})\n");

//foreach (var meal in mealsForCustomer)
//{
//    Console.WriteLine($"Meal ID: {meal.MealId}, Meal Name: {meal.MealName}, Canteen Name: {meal.CanteenName}");
//}

//Console.WriteLine("\n\n--------------------------------------------");
//Console.WriteLine("Query 3: Show the number of reservations for a canteen\n");

//string canteenName = "Kgl. Bibliotek";

//var reservations2 = from reservation in reservations.GetAll()
//    join reservationList in reservationLists.GetAll() on reservation.Id equals reservationList.ReservationId
//    join meal in meals.GetAll() on reservationList.MealId equals meal.Id
//    join canteen in canteens.GetAll() on meal.CanteenId equals canteen.Id
//    where canteen.CanteenName == canteenName
//    group reservation by meal.MealName into g
//    select new
//    {
//        MealName = g.Key,
//        ReservationCount = g.Count()
//    };

//Console.WriteLine($"The number of reservations for each meal at {canteenName} canteen:");
//foreach (var reservation in reservations2)
//{
//    Console.WriteLine($"{reservation.MealName}: {reservation.ReservationCount}");
//}

//Console.WriteLine("\n\n--------------------------------------------");
//Console.WriteLine("Query 4: Show the number of reservations for a canteen\n");

//// Query 1: Find all MealTypes for a given canteen
//string canteenName = "Kgl. Bibliotek";
//Canteen canteen = canteens.FindCanteenByName(canteenName);
//var mealTypes = meals.FindForCanteen(canteen.Id)
//    .GroupBy(m => m.Type)
//    .Select(g => g.Key)
//    .Distinct();

//Console.WriteLine($"Meal types available at {canteenName} canteen:");
//foreach (MealType mealType in mealTypes)
//{
//    Console.WriteLine(mealType);
//}

//Console.WriteLine("\n\n--------------------------------------------");
//Console.WriteLine("Query 5: Show the available (cancelled) meals for nearby canteens\n");

//string inputCanteenName = "Kgl. Bibliotek"; // replace with actual input canteen name

//var canteen = canteens.GetAll().FirstOrDefault(c => c.CanteenName == inputCanteenName);
//if (canteen == null)
//{
//    Console.WriteLine($"Canteen {inputCanteenName} not found");
//    return;
//}

//// find nearby canteens based on the zip code of the input canteen
//var nearbyCanteens = canteens.GetAllExcept(inputCanteenName).Where(c => c.ZipCode == canteen.ZipCode).ToList();
//if (nearbyCanteens.Count == 0)
//{
//    Console.WriteLine($"No nearby canteens found for {inputCanteenName}");
//    return;
//}

//// find all cancelled reservations and include the meals and canteens
//var cancelledReservations = reservations.GetAllCancelledReservations()
//    .AsQueryable()
//    .Include(r => r.ReservationLists)
//    .ThenInclude(rl => rl.Meal)
//    .ThenInclude(m => m.Canteen)
//    .ToList();

//// display the cancelled meals available at nearby canteens
//Console.WriteLine($"Cancelled meals available at nearby canteens from {inputCanteenName}:");
//foreach (var reservation in cancelledReservations)
//{
//    foreach (var reservationList in reservation.ReservationLists)
//    {
//        var meal = reservationList.Meal;
//        var canteenName = meal.Canteen.CanteenName;
//        var isNearbyCanteen = nearbyCanteens.Any(c => c.CanteenName == canteenName);
//        if (isNearbyCanteen && canteenName != inputCanteenName)
//        {
//            Console.WriteLine($"Canteen Name: {canteenName}, " +
//                              $"Zip Code: {meal.Canteen.ZipCode}, " +
//                              $"Meal Name: {meal.MealName}");
//        }
//    }
//}

Console.WriteLine("\n\n--------------------------------------------");
Console.WriteLine("Query 6: Show average rating of all canteens\n");

//var canteenRepo = new CanteenRepository();

//var canteenNames = canteenRepo.GetAllCanteenNames();
//var canteenAvgRatings = canteenRepo.GetCanteensAverageRatings();

//foreach (var name in canteenNames)
//{
//    Console.WriteLine($"{name}: {canteenAvgRatings[name]:F2}");
//}

Console.WriteLine("\n\n--------------------------------------------");
Console.WriteLine("Query 7: Show payroll for staff\n");

Canteen canteen = canteens.FindCanteenByName("Kgl. Bibliotek");

// Query 7: Get the payroll of all staff working in a canteen by canteen name
var canteenName = "Kgl. Bibliotek";
var payroll = canteens.GetStaffPayrollByCanteenName(canteenName);

if (payroll.Any())
{
    Console.WriteLine($"\nPayroll of staff working in {canteenName}:");
    Console.WriteLine("-------------------------------------");
    foreach (var (staffName, salary) in payroll)
    {
        Console.WriteLine($"{staffName}: {salary}");
    }
}
else
{
    Console.WriteLine($"No staff found working in {canteenName}.");
}



void SeedDb()
{


    // Create Canteens
    Console.WriteLine("Creating canteens");
    var canteen1 = new Canteen
    {
        CanteenName = "Kgl. Bibliotek",
        Address = "Nygade 6, Aarhus C",
        ZipCode = 8000,
        AVGRating = 4.9f,
        Staff = new List<Staff>(),
        Ratings = new List<Rating>(),
        Meals = new List<Meal>()
    };
    canteens.Insert(canteen1);

    var canteen2 = new Canteen
    {
        CanteenName = "Kemisk Canteen",
        Address = "Nørregade 10, Aarhus N",
        ZipCode = 8200,
        AVGRating = 4.5f,
        Staff = new List<Staff>(),
        Ratings = new List<Rating>(),
        Meals = new List<Meal>()
    };
    canteens.Insert(canteen2);

    var canteen3 = new Canteen
    {
        CanteenName = "Matematisk Canteen",
        Address = "Silkeborgvej 20, Aarhus V",
        ZipCode = 8210,
        AVGRating = 4.2f,
        Staff = new List<Staff>(),
        Ratings = new List<Rating>(),
        Meals = new List<Meal>()
    };
    canteens.Insert(canteen3);

    Console.WriteLine("Finished creating canteens");

    // Create Staff
    Console.WriteLine("Creating staff");
    
    var staff1 = new Staff
    {
        Name = "Jens B.",
        Title = "Cook",
        Salary = "30700",
        Canteen = canteen1
    };
    staffs.Insert(staff1);
    canteens.AddStaffToCanteen(canteen1.Id.ToString(), staff1);

    var staff2 = new Staff
    {
        Name = "Mette C.",
        Title = "Waiter",
        Salary = "29000",
        Canteen = canteen1
    };
    staffs.Insert(staff2);
    canteens.AddStaffToCanteen(canteen1.Id.ToString(), staff2);

    var staff3 = new Staff
    {
        Name = "Mads D. ",
        Title = "Waiter",
        Salary = "29000",
        Canteen = canteen1
    };
    staffs.Insert(staff3);
    canteens.AddStaffToCanteen(canteen1.Id.ToString(), staff3);

    var staff4 = new Staff
    {
        Name = "Lucile E.",
        Title = "Cook",
        Salary = "30700",
        Canteen = canteen1
    };
    staffs.Insert(staff4);
    canteens.AddStaffToCanteen(canteen1.Id.ToString(), staff4);

    Console.WriteLine("Finished creating staff");



    // Create Customers
    //List<Customer> customerList = new List<Customer>();
    Console.WriteLine("Creating customers");
    customers.AddNewCustomer("au030300","Søren Nielsen" , 4);
    customers.AddNewCustomer("au040400","Karen Christensen" , 3);
    customers.AddNewCustomer("au050500", "Peter Pedersen", 5);
    customers.AddNewCustomer("au060600", "Marianne Jensen", 5);
    customers.AddNewCustomer("au070700", "Jørgen Hansen", 2);
    customers.AddNewCustomer("au080800", "Gitte Madsen", 3);
    customers.AddNewCustomer("au090900", "Henrik Petersen", 4);
    Console.WriteLine("Finished creating customers");

    
    // Create Meals
    Console.WriteLine("Creating meals");
    meals.AddNewMeal("Green Curry", MealType.WarmDish, canteen1.Id);
    meals.AddNewMeal("Pizza", MealType.StreetFood, canteen1.Id);
    meals.AddNewMeal("Meatball Sandwich", MealType.StreetFood, canteen2.Id);
    meals.AddNewMeal("Soup", MealType.WarmDish, canteen1.Id);
    meals.AddNewMeal("Tacos", MealType.WarmDish, canteen1.Id);
    meals.AddNewMeal("Pizza", MealType.StreetFood, canteen2.Id);
    meals.AddNewMeal("Indian Curry", MealType.WarmDish, canteen1.Id);
    meals.AddNewMeal("Burger", MealType.StreetFood, canteen2.Id);
    Console.WriteLine("Finished creating meals");


    // Create Ratings
    Console.WriteLine("Creating ratings");

    var rating1 = new Rating
    {
        CustomerId = customers.FindCustomerByAuId("au030300").Id,
        RatingValue = 4,
        CanteenId = canteens.FindCanteenByName("Kgl. Bibliotek").Id,
        Datetime = DateTime.Today.AddDays(-10)
    };
    

    var rating2 = new Rating
    {
        CustomerId = customers.FindCustomerByAuId("au040400").Id,
        RatingValue = 3,
        CanteenId = canteens.FindCanteenByName("Kemisk Canteen").Id,
        Datetime = DateTime.Today.AddDays(-9)
    };
    ratings.AddNewRating(rating2.CustomerId, rating2.RatingValue, rating2.CanteenId, rating2.Datetime);

    var rating3 = new Rating
    {
        CustomerId = customers.FindCustomerByAuId("au050500").Id,
        RatingValue = 5,
        CanteenId = canteens.FindCanteenByName("Matematisk Canteen").Id,
        Datetime = DateTime.Today.AddDays(-8)
    };
    ratings.AddNewRating(rating3.CustomerId, rating3.RatingValue, rating3.CanteenId, rating3.Datetime);

    var rating4 = new Rating
    {
        CustomerId = customers.FindCustomerByAuId("au060600").Id,
        RatingValue = 5,
        CanteenId = canteens.FindCanteenByName("Matematisk Canteen").Id,
        Datetime = DateTime.Today.AddDays(-7)
    };
    ratings.AddNewRating(rating4.CustomerId, rating4.RatingValue, rating4.CanteenId, rating4.Datetime);

    var rating5 = new Rating
    {
        CustomerId = customers.FindCustomerByAuId("au070700").Id,
        RatingValue = 2,
        CanteenId = canteens.FindCanteenByName("Kemisk Canteen").Id,
        Datetime = DateTime.Today.AddDays(-6)
    };
    ratings.AddNewRating(rating5.CustomerId, rating5.RatingValue, rating5.CanteenId, rating5.Datetime);

    var rating6 = new Rating
    {
        CustomerId = customers.FindCustomerByAuId("au080800").Id,
        RatingValue = 3,
        CanteenId = canteens.FindCanteenByName("Kgl. Bibliotek").Id,
        Datetime = DateTime.Today.AddDays(-5)
    };
    ratings.AddNewRating(rating6.CustomerId, rating6.RatingValue, rating6.CanteenId, rating6.Datetime);

    var rating7 = new Rating
    {
        CustomerId = customers.FindCustomerByAuId("au090900").Id,
        RatingValue = 4,
        CanteenId = canteens.FindCanteenByName("Matematisk Canteen").Id,
        Datetime = DateTime.Today.AddDays(-4)
    };
    ratings.AddNewRating(rating7.CustomerId, rating7.RatingValue, rating7.CanteenId, rating7.Datetime);

    Console.WriteLine("Finished creating ratings");



    // Create Reservations
    Console.WriteLine("Creating reservations");

    var reservation1 = new Reservation
    {
        Status = ReservationStatus.Cancelled ,
        ReservationTime = DateTime.Today.AddDays(1),
        CustomerId = customers.FindCustomerByAuId("au030300").Id,
        CanteenId = canteens.FindCanteenByName("Kgl. Bibliotek").Id,
        ReservationLists = new List<ReservationList>
        {
            new ReservationList
            {
                MealId = meals.FindMealsByName("Green Curry").FirstOrDefault().Id,

            }
        }
    };

    // Try to create the reservation
    if (reservations.CreateReservation(reservation1))
    {
        Console.WriteLine("Reservation created successfully.");
    }
    else
    {
        Console.WriteLine("Failed to create reservation.");
    }

    Console.WriteLine("Finished creating reservations");

    var reservation2 = new Reservation
    {
        Status = ReservationStatus.Reserved,
        ReservationTime = DateTime.Today.AddDays(1),
        CustomerId = customers.FindCustomerByAuId("au040400").Id,
        CanteenId = canteens.FindCanteenByName("Matematisk Canteen").Id,
        ReservationLists = new List<ReservationList>
        {
            new ReservationList
            {
                MealId = meals.FindMealsByName("Green Curry").FirstOrDefault().Id,

            }
        }
    };

    // Try to create the reservation
    if (reservations.CreateReservation(reservation2))
    {
        Console.WriteLine("Reservation created successfully.");
    }
    else
    {
        Console.WriteLine("Failed to create reservation.");
    }

    Console.WriteLine("Finished creating reservations");


    

    // Create ReservationLists
    Console.WriteLine("Creating reservation lists");

    // Create Meals list
    Console.WriteLine("Creating meals");
    List<Meal> meals2 = new List<Meal>();
    MealRepository mealRepository = new MealRepository();
    meals2.AddRange(mealRepository.FindAll());
    Console.WriteLine("Finished creating meals");

    // Create ReservationLists
    Console.WriteLine("Creating reservation lists");
    var greenCurryMeal = meals.FindMealsByName("Green Curry").FirstOrDefault();
    if (greenCurryMeal == null)
    {
        return;
    }
    var reservationList1 = new ReservationList
    {
        ReservationId = reservation1.Id,
        MealId = greenCurryMeal.Id

    };
    reservationLists.Insert(reservationList1);

    Console.WriteLine("Finished creating reservation lists");


    Console.WriteLine("Seed data created successfully.");


};




