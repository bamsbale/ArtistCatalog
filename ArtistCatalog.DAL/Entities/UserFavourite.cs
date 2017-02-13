using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistCatalog.DAL.Entities
{
    public class UserFavourite
    {
        [Column(Order = 0), Key]
        public Guid AspNetUserId { get; set; }
        [Column(Order = 1), Key]
        public Guid ReleaseId { get; set; }
        public string Title { get; set; }
        public string ReleaseYear { get; set; }
        public string Label { get; set; }
        public int NoOfTracks { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
