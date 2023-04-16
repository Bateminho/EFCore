using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public string CustomerCPR { get; set; } //Foreign key
        public int CanteenID { get; set; }
        public int MealID { get; set; }
        public bool Cancelled { get; set; }
        public DateTime Datetime { get; set; }

        // Navigation properties
        public List<Meal> Meals { get; set; }
        public Customer Customer { get; set; }
    }
}
