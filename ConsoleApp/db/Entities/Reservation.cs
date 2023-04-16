using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.db.Entities
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime Reservationtime { get; set; }

        // Navigation properties
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
		public int CanteenId { get; set; }
		public Canteen? Canteen { get; set; }
        public ICollection<ReservationList> ReservationLists { get; set; }
	}

    public enum ReservationStatus
    {
	    Reserved,
	    Canceled
    }
}
