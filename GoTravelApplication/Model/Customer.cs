using System;
using System.Collections.Generic;

#nullable disable

namespace GoTravelApplication.Model
{
    public partial class Customer
    {
        public Customer()
        {
            CartBookings = new HashSet<CartBooking>();
            CustomerBookings = new HashSet<CustomerBooking>();
            CustomerReviews = new HashSet<CustomerReview>();
        }

        public int CustomerId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<CartBooking> CartBookings { get; set; }
        public virtual ICollection<CustomerBooking> CustomerBookings { get; set; }
        public virtual ICollection<CustomerReview> CustomerReviews { get; set; }
    }
}
