using System;
using System.Collections.Generic;

#nullable disable

namespace GoTravelApplication.Model
{
    public partial class Administrator
    {
        public Administrator()
        {
            AdminResponses = new HashSet<AdminResponse>();
        }

        public int AdminId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<AdminResponse> AdminResponses { get; set; }
    }
}
