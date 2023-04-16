using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Customer
    {
        public string CustomerCPR { get; set; }
        public string Name { get; set; }
        public int MealID { get; set; }
        public int Rating { get; set; }

        // Navigation properties
        public List<Rating> Ratings { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
