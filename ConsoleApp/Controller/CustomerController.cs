//using System;
//using System.Collections.Generic;
//using System.Linq;
//using ConsoleApp.db;
//using ConsoleApp.db.Entities;
//using Microsoft.EntityFrameworkCore;

//namespace ConsoleApp.Controllers
//{
//	public class CustomerController : Controllers
//	{
//		private readonly MyDBContext _db;

//		public CustomerController(MyDBContext context)
//		{
//			_db = new MyDBContext();
//			_db.Database.EnsureCreated();
//		}

//		public void Create(Customer customer)
//		{
//			if (_db.Customers.Any(c => c.CustomerCPR == customer.CustomerCPR))
//			{
//				Console.WriteLine($"Customer with CPR {customer.CustomerCPR} already exists in the database");
//				return;
//			}

//			_db.Customers.Add(customer);
//			_db.SaveChanges();

//			Console.WriteLine($"Customer {customer.Name} with CPR {customer.CustomerCPR} has been added to the database");
//		}

//		public Customer Read(int customerId)
//		{
//			var customer = _db.Customers.Find(customerId);

//			if (customer == null)
//			{
//				Console.WriteLine($"Customer with ID {customerId} was not found in the database");
//			}

//			return customer;
//		}

//		public List<Customer> ReadAll()
//		{
//			var customers = _db.Customers.ToList();

//			if (customers.Count == 0)
//			{
//				Console.WriteLine("There are no customers in the database");
//			}

//			return customers;
//		}

//		public void Update(Customer customer)
//		{
//			var existingCustomer = _db.Customers.Find(customer.CustomerId);

//			if (existingCustomer == null)
//			{
//				Console.WriteLine($"Customer with ID {customer.CustomerId} was not found in the database");
//				return;
//			}

//			if (_db.Customers.Any(c => c.CustomerCPR == customer.CustomerCPR && c.CustomerId != customer.CustomerId))
//			{
//				Console.WriteLine($"Customer with CPR {customer.CustomerCPR} already exists in the database");
//				return;
//			}

//			existingCustomer.Name = customer.Name;
//			existingCustomer.CustomerCPR = customer.CustomerCPR;
//			existingCustomer.Rating = customer.Rating;

//			_db.SaveChanges();

//			Console.WriteLine($"Customer with ID {customer.CustomerId} has been updated in the database");
//		}

//		public void Delete(int customerId)
//		{
//			var customer = _db.Customers.Find(customerId);

//			if (customer == null)
//			{
//				Console.WriteLine($"Customer with ID {customerId} was not found in the database");
//				return;
//			}

//			_db.Customers.Remove(customer);
//			_db.SaveChanges();

//			Console.WriteLine($"Customer with ID {customerId} has been deleted from the database");
//		}
//	}
//}
