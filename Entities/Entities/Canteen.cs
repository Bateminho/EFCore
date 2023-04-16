using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Canteen
    {
        public int CanteenID { get; set; }
        public int MealID { get; set; } 
        public string Location { get; set; }
        public float AVGRating { get; set; }
        public string CanteenName { get; set; }

        // Navigation properties
        public ICollection<Rating> Ratings { get; set; }
        public AutomatedDeliverySystem AutomatedDeliverySystem { get; set; }
        public List<Meal> Meals { get; set; }

        //Foreign key
        public string DropInGuest { get; set; }
    }
}
