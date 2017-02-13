using ArtistCatalog.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtistCatalog.DAL.Entities;

namespace ArtistCatalog.Test.Fakes
{
    public class FakeArtistCatalogRepository : IArtistCatalogRepository
    {
        private IArtistCatalogDbContext _db;
        public FakeArtistCatalogRepository(IArtistCatalogDbContext db)
        {
            _db = db;
        }


        // General
        public bool SaveAll()
        {
            return true;
        }


        // Artist
        public IQueryable<Artist> GetArtists()
        {
            return _db.Query<Artist>();
        }
        public Artist GetArtist(Guid uid)
        {
            return _db.Query<Artist>().FirstOrDefault();
        }


        // UserFavourite
        public IEnumerable<UserFavourite> GetUserFavourites(Guid aspNetUserId, bool isActive)
        {
            throw new NotImplementedException();
        }
        public bool AddOrUpdateUserFavourite(UserFavourite userFavourite)
        {
            throw new NotImplementedException();
        }
        public bool RemoveFavourite(Guid aspNetUserId, Guid releaseId)
        {
            throw new NotImplementedException();
        }

        
    }
}
