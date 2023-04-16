using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Rating
    {
        public int RatingID { get; set; }
        [MaxLength(11)]
        public string CustomerCPR  { get; set; } // foreign key
        public float RatingValue { get; set; }
        public int CanteenID { get; set; }
        public DateTime Datetime { get; set; }

        // Navigation properties
        public ICollection<Canteen> Canteens { get; set; }
        public Customer Customer { get; set; }


    }
}
