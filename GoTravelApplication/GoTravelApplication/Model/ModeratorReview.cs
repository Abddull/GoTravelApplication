using System;
using System.Collections.Generic;

#nullable disable

namespace GoTravelApplication.Model
{
    public partial class ModeratorReview
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public int ModeratorId { get; set; }

        public virtual Moderator Moderator { get; set; }
    }
}
