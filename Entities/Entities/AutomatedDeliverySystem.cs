using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class AutomatedDeliverySystem
    {
        public int MealID { get; set; }
        public string CanteenName { get; set; }
        public string DropInGuest { get; set; }
        public int CanteenID { get; set; }

        // Navigation properties
        public List<Canteen> Canteens { get; set; }
    }
}
