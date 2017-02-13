using ArtistCatalog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistCatalog.DAL
{
    public interface IArtistCatalogRepository
    {
        // General
        bool SaveAll();

        // Artist
        IQueryable<Artist> GetArtists();
        Artist GetArtist(Guid uid);

        // UserFavourite
        IEnumerable<UserFavourite> GetUserFavourites(Guid aspNetUserId, bool isActive);
        bool AddOrUpdateUserFavourite(UserFavourite userFavourite);
        bool RemoveFavourite(Guid aspNetUserId, Guid releaseId);
    }
}
