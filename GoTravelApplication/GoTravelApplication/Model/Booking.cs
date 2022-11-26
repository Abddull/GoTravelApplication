using System;
using System.Collections.Generic;

#nullable disable

namespace GoTravelApplication.Model
{
    public partial class Booking
    {
        public Booking()
        {
            CartBookings = new HashSet<CartBooking>();
            CustomerBookings = new HashSet<CustomerBooking>();
            Pictures = new HashSet<Picture>();
        }

        public int BookingId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<CartBooking> CartBookings { get; set; }
        public virtual ICollection<CustomerBooking> CustomerBookings { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
    }
}
