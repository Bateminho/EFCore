using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.db.Entities
{
    public class Customer
    {
	    public int CustomerId { get; set; }
	    public string CustomerCPR { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        
        
		// Navigation properties
		public List<Rating> Ratings { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}
