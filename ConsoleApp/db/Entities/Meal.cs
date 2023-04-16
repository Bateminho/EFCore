using System;
using System.Collections.Generic;
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
		WarmDish,
		StreetFood,
		JustInTime
	}
}