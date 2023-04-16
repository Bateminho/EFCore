using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Meal
    {
        public int MealID { get; set; }
        public string MealName { get; set; }
        public string MealType { get; set; }
        public bool Cancelled { get; set; }

        // Navigation properties
        public Reservation Reservation { get; set; }
        public Canteen Canteen { get; set; }

        //Foreign keys
        public int ReservationID { get; set; }
        public int CanteenID { get; set; }
    }
}
