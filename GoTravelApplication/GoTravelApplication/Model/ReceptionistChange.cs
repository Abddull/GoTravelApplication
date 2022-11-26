using System;
using System.Collections.Generic;

#nullable disable

namespace GoTravelApplication.Model
{
    public partial class ReceptionistChange
    {
        public int ChangeId { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public DateTime ChangeTime { get; set; }
        public int ReceptionistId { get; set; }
        public int CustomerBookingId { get; set; }

        public virtual CustomerBooking CustomerBooking { get; set; }
        public virtual Receptionist Receptionist { get; set; }
    }
}
