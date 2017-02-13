using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtistCatalog.Web.Models
{
    public class UserFavouriteModel
    {
        public Guid AspNetUserId { get; set; }
        public Guid ReleaseId { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}