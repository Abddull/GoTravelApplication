using System;
using System.Collections.Generic;

#nullable disable

namespace GoTravelApplication.Model
{
    public partial class Moderator
    {
        public Moderator()
        {
            AdminResponses = new HashSet<AdminResponse>();
            ModRequests = new HashSet<ModRequest>();
            ModeratorReviews = new HashSet<ModeratorReview>();
        }

        public int ModeratorId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<AdminResponse> AdminResponses { get; set; }
        public virtual ICollection<ModRequest> ModRequests { get; set; }
        public virtual ICollection<ModeratorReview> ModeratorReviews { get; set; }
    }
}
