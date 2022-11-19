using System;
using System.Collections.Generic;

#nullable disable

namespace GoTravelApplication.Model
{
    public partial class Receptionist
    {
        public Receptionist()
        {
            ReceptionistChanges = new HashSet<ReceptionistChange>();
        }

        public int ReceptionistId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<ReceptionistChange> ReceptionistChanges { get; set; }
    }
}
