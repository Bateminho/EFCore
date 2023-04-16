using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.db.Entities
{
	public class ReservationList
	{
		[Key]
		public int ReservationId { get; set; }
		
		public Reservation Reservation { get; set; }
		[Key]
		public int MealId { get; set; }
		public Meal Meal { get; set; }
	}
}
