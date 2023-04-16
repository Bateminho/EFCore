using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp.db.Entities
{
    public class Canteen
    {
        public int CanteenId { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
        public float AVGRating { get; set; }
        public string CanteenName { get; set; }

        // Navigation properties
        public ICollection<Rating> Ratings { get; set; }
        public List<Meal> Meals { get; set; }

    }
}
