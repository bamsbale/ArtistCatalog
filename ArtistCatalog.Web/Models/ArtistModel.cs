using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtistCatalog.Web.Models
{
    public class ArtistModel
    {
        public Guid UID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string[] Alias { get; set; }
        public string ReleasesUrl { get; set; }
    }
}