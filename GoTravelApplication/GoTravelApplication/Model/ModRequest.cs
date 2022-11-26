using System;
using System.Collections.Generic;

#nullable disable

namespace GoTravelApplication.Model
{
    public partial class ModRequest
    {
        public int RequestId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime RequestTime { get; set; }
        public int ModeratorId { get; set; }

        public virtual Moderator Moderator { get; set; }
    }
}
