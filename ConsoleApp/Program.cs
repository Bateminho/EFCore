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
    //Delete existing data
    canteens.DeleteAll();
    customers.DeleteAll();
    meals.DeleteAll();
    reservations.DeleteAll();
    reservationLists.DeleteAll();
    staffs.DeleteAll();
}

SeedDb();


//Query 1 Get the days menu options for a canteen  --------------------------------------------------------------------------------
Console.WriteLine("\n\n--------------------------------------------");
Console.WriteLine("Query 1: Find meals by canteen name\n");
Canteen canteenq1 = canteens.FindCanteenByName("Kgl. Bibliotek");
List<Meal> mealsForCanteen = meals.FindForCanteen(canteenq1.Id)
    .Select(meals => new Meal() { MealName = meals.MealName, Type = meals.Type })
    .ToList();
foreach (Meal meal in mealsForCanteen)
{
    Console.WriteLine($"Meal Name: {meal.MealName}, Type: {meal.Type}");
}

//Query 2 Get the reservation for given user  ---------------------------------------------------------------------------
Console.WriteLine("\n\n--------------------------------------------");
Console.WriteLine("Query 2: Find reservation for a customer\n");

var customer = customers.FindCustomerByAuId("au030300");

var reservationsForCustomer = reservations.GetReservationsByCustomerId(customer.Id).ToList();

var mealsForCustomer = (from reservationList in reservationLists.GetAll()
    join reservation in reservations.GetAll() on reservationList.ReservationId equals reservation.Id
    join meal in meals.GetAll() on reservationList.MealId equals meal.Id
    join canteen2 in canteens.GetAll() on meal.CanteenId equals canteen2.Id
    where reservation.CustomerId == customer.Id
    select new { MealId = meal.Id, MealName = meal.MealName, CanteenName = canteen2.CanteenName }).ToList();


foreach (var meal in mealsForCustomer)
{
    Console.WriteLine($"Meal ID: {meal.MealId} Name: {meal.MealName} Canteen: {meal.CanteenName}");
}

//Query 3 For a canteen given as input, show the number of reservations ---------------------------------------------------------------------

Console.WriteLine("\n\n--------------------------------------------");
Console.WriteLine("Query 3: Show the number of reservations for a canteen\n");

string canteenName = "Kgl. Bibliotek";

var reservations2 = from reservation in reservations.GetAll()
                    join reservationList in reservationLists.GetAll() on reservation.Id equals reservationList.ReservationId
                    join meal in meals.GetAll() on reservationList.MealId equals meal.Id
                    join canteen in canteens.GetAll() on meal.CanteenId equals canteen.Id
                    where canteen.CanteenName == canteenName
                    group reservation by meal.MealName into g
                    select new
                    {
                        MealName = g.Key,
                        ReservationCount = g.Count()
                    };

Console.WriteLine($"The number of reservations for each meal at {canteenName} canteen:");
foreach (var reservation in reservations2)
{
    Console.WriteLine($"{reservation.MealName}: {reservation.ReservationCount}");
}
//Query 4 For a canteen given as input, show the just-in-time meal options --------------------------------------------------------------------

Console.WriteLine("\n\n--------------------------------------------");
Console.WriteLine("Query 4: Show cancelled meals from other canteens\n");


string canteenNameQ4 = "Kgl. Bibliotek";
Canteen canteenQ4 = canteens.FindCanteenByName(canteenNameQ4);
var mealTypes = meals.FindForCanteen(canteenQ4.Id)
    .GroupBy(m => m.Type)
    .Select(g => new { Type = g.Key, MealNames = g.Select(m => m.MealName).ToList() })
    .ToList();

Console.WriteLine($"Meal types available at {canteenNameQ4} canteen:");
foreach (var mealType in mealTypes)
{
    //Console.WriteLine($"Type: {mealType.Type}");
    Console.WriteLine($"Meal names: {string.Join(", ", mealType.MealNames)}");
}

//// Query 5 or a canteen given as input, show the the available (canceled) -------------------------------------------------------------------

Console.WriteLine("\n\n--------------------------------------------");
Console.WriteLine("Query 5: Show the available (cancelled) meals for nearby canteens\n");
var reservationRepo = new ReservationRepository();
reservationRepo.PrintCancelledReservationsFromOtherCanteens("Kgl. Bibliotek");


//Query 6 -----------------------------------------------------------------------------------------------------------------------
Console.WriteLine("\n\n--------------------------------------------");
Console.WriteLine("Query 6: Show average rating of all canteens\n");

var ratingRepo = new RatingRepository(canteens);
var averageRatings = ratingRepo.GetAverageRatingValuesForAllCanteens();

foreach (var rating in averageRatings)
{
    Console.WriteLine($"{rating.Key}: {rating.Value}");
}


//// Query 7: Get the payroll of all staff working in a canteen by canteen name
Console.WriteLine("\n\n--------------------------------------------");
Console.WriteLine("Query 7: Show payroll for staff\n");

CanteenRepository repo = new CanteenRepository();
repo.PrintStaffInformation("Kgl. Bibliotek");
 



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
        Staff = new Staff[]
        {
            new Staff
            {
                Id = ObjectId.GenerateNewId(),
                Name = "Jens B.",
                Title = "Cook",
                Salary = "30700"
            },
            new Staff
            {
                Id = ObjectId.GenerateNewId(),
                Name = "Mette C.",
                Title = "Waiter",
                Salary = "29000"
            },
            new Staff
            {
                Id = ObjectId.GenerateNewId(),
                Name = "Mads D.",
                Title = "Waiter",
                Salary = "29000"
            },
            new Staff
            {
                Id = ObjectId.GenerateNewId(),
                Name = "Lucile E.",
                Title = "Cook",
                Salary = "30700"
            }
        }
        
    };
    
    canteens.Insert(canteen1);

    var canteen2 = new Canteen
    {
        CanteenName = "Kemisk Canteen",
        Address = "Nørregade 10, Aarhus N",
        ZipCode = 8200,
        AVGRating = 4.5f,
        Staff = new Staff[]
        {
            new Staff
            {
                Name = "John Smith",
                Title = "Chef",
                Salary = "40000"
            }
        }
    };
    canteens.Insert(canteen2);

    var canteen3 = new Canteen()
    {
        CanteenName = "Matematisk Canteen",
        Address = "Silkeborgvej 20, Aarhus V",
        ZipCode = 8210,
        AVGRating = 4.2f,
    };

    canteens.Insert(canteen3);


    Console.WriteLine("Finished creating canteens and staff");



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
    meals.AddNewMeal("Green Curry", "WarmDish", canteen1.Id);
    meals.AddNewMeal("Pizza", "StreetFood", canteen1.Id);
    meals.AddNewMeal("Meatball Sandwich", "StreetFood", canteen2.Id);
    meals.AddNewMeal("Soup", "WarmDish", canteen1.Id);
    meals.AddNewMeal("Red Curry", "WarmDish", canteen1.Id);
    meals.AddNewMeal("Pita m. Tun", "StreetFood", canteen2.Id);
    meals.AddNewMeal("Indian Curry", "WarmDish", canteen1.Id);
    meals.AddNewMeal("Burger", "StreetFood", canteen3.Id);
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
    ratings.AddNewRating(rating1.CustomerId, rating1.RatingValue, rating1.CanteenId, rating1.Datetime);
    canteens.AddRatingToCanteen(rating1.CanteenId, rating1.RatingValue);

    var rating2 = new Rating
    {
        CustomerId = customers.FindCustomerByAuId("au040400").Id,
        RatingValue = 3,
        CanteenId = canteens.FindCanteenByName("Kemisk Canteen").Id,
        Datetime = DateTime.Today.AddDays(-9)
    };
    ratings.AddNewRating(rating2.CustomerId, rating2.RatingValue, rating2.CanteenId, rating2.Datetime);
    canteens.AddRatingToCanteen(rating2.CanteenId, rating2.RatingValue);

    var rating3 = new Rating
    {
        CustomerId = customers.FindCustomerByAuId("au050500").Id,
        RatingValue = 5,
        CanteenId = canteens.FindCanteenByName("Matematisk Canteen").Id,
        Datetime = DateTime.Today.AddDays(-8)
    };
    ratings.AddNewRating(rating3.CustomerId, rating3.RatingValue, rating3.CanteenId, rating3.Datetime);
    canteens.AddRatingToCanteen(rating3.CanteenId, rating3.RatingValue);

    var rating4 = new Rating
    {
        CustomerId = customers.FindCustomerByAuId("au060600").Id,
        RatingValue = 5,
        CanteenId = canteens.FindCanteenByName("Matematisk Canteen").Id,
        Datetime = DateTime.Today.AddDays(-7)
    };
    ratings.AddNewRating(rating4.CustomerId, rating4.RatingValue, rating4.CanteenId, rating4.Datetime);
    canteens.AddRatingToCanteen(rating4.CanteenId, rating4.RatingValue);

    var rating5 = new Rating
    {
        CustomerId = customers.FindCustomerByAuId("au070700").Id,
        RatingValue = 2,
        CanteenId = canteens.FindCanteenByName("Kemisk Canteen").Id,
        Datetime = DateTime.Today.AddDays(-6)
    };
    ratings.AddNewRating(rating5.CustomerId, rating5.RatingValue, rating5.CanteenId, rating5.Datetime);
    canteens.AddRatingToCanteen(rating5.CanteenId, rating5.RatingValue);

    var rating6 = new Rating
    {
        CustomerId = customers.FindCustomerByAuId("au080800").Id,
        RatingValue = 3,
        CanteenId = canteens.FindCanteenByName("Kgl. Bibliotek").Id,
        Datetime = DateTime.Today.AddDays(-5)
    };
    ratings.AddNewRating(rating6.CustomerId, rating6.RatingValue, rating6.CanteenId, rating6.Datetime);
    canteens.AddRatingToCanteen(rating6.CanteenId, rating6.RatingValue);

    var rating7 = new Rating
    {
        CustomerId = customers.FindCustomerByAuId("au090900").Id,
        RatingValue = 4,
        CanteenId = canteens.FindCanteenByName("Matematisk Canteen").Id,
        Datetime = DateTime.Today.AddDays(-4)
    };
    ratings.AddNewRating(rating7.CustomerId, rating7.RatingValue, rating7.CanteenId, rating7.Datetime);
    canteens.AddRatingToCanteen(rating7.CanteenId, rating7.RatingValue);

    Console.WriteLine("Finished creating ratings");



    // Create Reservations
    Console.WriteLine("Creating reservations");

    var reservation1 = new Reservation
    {
        ReservationStatus = "Reserved",
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

    

    var reservation2 = new Reservation
    {
        ReservationStatus = "Cancelled",
        ReservationTime = DateTime.Today.AddDays(1),
        CustomerId = customers.FindCustomerByAuId("au040400").Id,
        CanteenId = canteens.FindCanteenByName("Matematisk Canteen").Id,
        ReservationLists = new List<ReservationList>
        {
            new ReservationList
            {
                MealId = meals.FindMealsByName("Pizza").FirstOrDefault().Id,

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
    //--------------------------------------------------------------------------------------------------------------
    // Create Reservations
    Console.WriteLine("Creating reservations");

    var reservation3 = new Reservation
    {
        ReservationStatus = "Reserved",
        ReservationTime = DateTime.Today.AddDays(1),
        CustomerId = customers.FindCustomerByAuId("au060600").Id,
        CanteenId = canteens.FindCanteenByName("Kgl. Bibliotek").Id,
        ReservationLists = new List<ReservationList>
        {
            new ReservationList
            {
                MealId = meals.FindMealsByName("Red Curry").FirstOrDefault().Id,

            }
        }
    };

    // Try to create the reservation
    if (reservations.CreateReservation(reservation3))
    {
        Console.WriteLine("Reservation created successfully.");
    }
    else
    {
        Console.WriteLine("Failed to create reservation.");
    }



    var reservation4 = new Reservation
    {
        ReservationStatus = "Cancelled",
        ReservationTime = DateTime.Today.AddDays(1),
        CustomerId = customers.FindCustomerByAuId("au030300").Id,
        CanteenId = canteens.FindCanteenByName("Matematisk Canteen").Id,
        ReservationLists = new List<ReservationList>
        {
            new ReservationList
            {
                MealId = meals.FindMealsByName("Pizza").FirstOrDefault().Id,

            }
        }
    };

    // Try to create the reservation
    if (reservations.CreateReservation(reservation4))
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
    List<Meal> meals5 = new List<Meal>();
    MealRepository mealRepository2 = new MealRepository();
    meals2.AddRange(mealRepository2.FindAll());
    Console.WriteLine("Finished creating meals");

    // Create ReservationLists
    Console.WriteLine("Creating reservation lists");
    var redCurryMeal = meals.FindMealsByName("Red Curry").FirstOrDefault();
    if (redCurryMeal == null)
    {
        return;
    }
    var reservationList2 = new ReservationList
    {
        ReservationId = reservation2.Id,
        MealId = redCurryMeal.Id

    };
    reservationLists.Insert(reservationList2);

    Console.WriteLine("Finished creating reservation lists");

   
    var reservation5 = new Reservation
    {
        ReservationStatus = "Reserved",
        ReservationTime = DateTime.Today.AddDays(1),
        CustomerId = customers.FindCustomerByAuId("au030300").Id,
        CanteenId = canteens.FindCanteenByName("Kgl. Bibliotek").Id,
        ReservationLists = new List<ReservationList>
        {
            new ReservationList
            {
                MealId = meals.FindMealsByName("Burger").FirstOrDefault().Id,

            }
        }
    };

    // Try to create the reservation
    if (reservations.CreateReservation(reservation5))
    {
        Console.WriteLine("Reservation created successfully.");
    }
    else
    {
        Console.WriteLine("Failed to create reservation.");
    }


    var reservation6 = new Reservation
    {
        ReservationStatus = "Cancelled",
        ReservationTime = DateTime.Today.AddDays(1),
        CustomerId = customers.FindCustomerByAuId("au060600").Id,
        CanteenId = canteens.FindCanteenByName("Matematisk Canteen").Id,
        ReservationLists = new List<ReservationList>
        {
            new ReservationList
            {
                MealId = meals.FindMealsByName("Pizza").FirstOrDefault().Id,

            }
        }
    };

    // Try to create the reservation
    if (reservations.CreateReservation(reservation6))
    {
        Console.WriteLine("Reservation created successfully.");
    }
    else
    {
        Console.WriteLine("Failed to create reservation.");
    }

    
    List<Meal> meals7 = new List<Meal>();
    MealRepository mealRepository3 = new MealRepository();
    meals5.AddRange(mealRepository3.FindAll());
    Console.WriteLine("Finished creating meals");

    // Create ReservationLists
   
    var pitabread = meals.FindMealsByName("Pita m. Tun").FirstOrDefault();
    if (pitabread == null)
    {
        return;
    }
    var reservationList5 = new ReservationList
    {
        ReservationId = reservation5.Id,
        MealId = pitabread.Id

    };
    reservationLists.Insert(reservationList5);

    Console.WriteLine("Finished creating reservation lists");



    //--------------------------------------------------------------------------------------------------------------
    Console.WriteLine("Seed data created successfully.");



};




