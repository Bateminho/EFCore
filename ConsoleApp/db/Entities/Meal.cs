using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.db.Entities
{
	public class Meal
	{
		public int MealId { get; set; }
		public string MealName { get; set; }
		
		public MealType Type { get; set; }

		// Navigation properties
		public int CanteenId { get; set; } 
		public virtual Canteen Canteen { get; set; }
		
		public List<ReservationList> ReservationLists { get; set; }
	}

	public enum MealType
	{
		[Display (Name = "Warm Dish")]
		WarmDish,
		[Display (Name = "Street Food")]
		StreetFood,
		[Display (Name = "Just In Time")]
		JustInTime
	}
}