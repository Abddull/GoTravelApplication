using System;
using System.Collections.Generic;

#nullable disable

namespace GoTravelApplication.Model
{
    public partial class AdminResponse
    {
        public int ResponseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ResponseTime { get; set; }
        public int ModeratorId { get; set; }
        public int AdminId { get; set; }

        public virtual Administrator Admin { get; set; }
        public virtual Moderator Moderator { get; set; }
    }
}
