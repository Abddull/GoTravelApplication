using System;
using System.Collections.Generic;

#nullable disable

namespace GoTravelApplication.Model
{
    public partial class CartBooking
    {
        public int CartId { get; set; }
        public int BookingId { get; set; }
        public int CustomerId { get; set; }

        public virtual Booking Booking { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
