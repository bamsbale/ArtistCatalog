using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistCatalog.DAL.Entities
{
    public class Artist
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Aliases { get; set; }
    }
}
