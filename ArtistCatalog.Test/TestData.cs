using ArtistCatalog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistCatalog.Test
{
    class TestData
    {
        public static IQueryable<Artist> Artists
        {
            get
            {
                var artists = new List<Artist>();
                for (int i = 0; i < 10; i++)
                {
                    var artist = new Artist();
                    artist.ID = Guid.NewGuid();
                    artist.Name = string.Format("Artist {0}", i);
                    artist.Aliases = string.Format("Artist Alias {0}", i);
                    artist.Country = i / 2 == 0 ? "SL" : "US";
                    artists.Add(artist);
                }

                return artists.AsQueryable();
            }
        }
    }
}
