using System;
using System.Collections.Generic;

#nullable disable

namespace GoTravelApplication.Model
{
    public partial class Picture
    {
        public int PictureId { get; set; }
        public string FileName { get; set; }
        public int BookingId { get; set; }

        public virtual Booking Booking { get; set; }
    }
}
