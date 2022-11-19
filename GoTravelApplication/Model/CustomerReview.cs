using System;
using System.Collections.Generic;

#nullable disable

namespace GoTravelApplication.Model
{
    public partial class CustomerReview
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
