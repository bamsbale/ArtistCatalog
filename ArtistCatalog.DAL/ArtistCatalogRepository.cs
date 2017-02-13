using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtistCatalog.DAL.Entities;

namespace ArtistCatalog.DAL
{
    public class ArtistCatalogRepository : IArtistCatalogRepository
    {
        private IArtistCatalogDbContext _db;
        public ArtistCatalogRepository(IArtistCatalogDbContext db)
        {
            _db = db;
        }

        // General
        public bool SaveAll()
        {
            try
            {
                return _db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Artist
        public IQueryable<Artist> GetArtists()
        {
            return _db.Query<Artist>();
        }
        public Artist GetArtist(Guid uid)
        {
            return _db.Query<Artist>().Where(a => a.ID == uid).FirstOrDefault();
        }

        // UserFavourite
        public IEnumerable<UserFavourite> GetUserFavourites(Guid aspNetUserId, bool isActive)
        {
            return _db.Query<UserFavourite>().Where(u => u.AspNetUserId == aspNetUserId && u.IsActive == isActive);
        }
        public bool AddOrUpdateUserFavourite(UserFavourite userFavourite)
        {
            if (userFavourite == null)
                return false;

            try
            {
                UserFavourite _userFavourite = _db.Query<UserFavourite>().Where(u => u.AspNetUserId == userFavourite.AspNetUserId && u.ReleaseId == userFavourite.ReleaseId).FirstOrDefault();
                if(_userFavourite != null)
                {
                    _userFavourite.Title = userFavourite.Title;
                    _userFavourite.ReleaseYear = userFavourite.ReleaseYear;
                    _userFavourite.Label = userFavourite.Label;
                    _userFavourite.NoOfTracks = userFavourite.NoOfTracks;
                    _userFavourite.ModifiedOn = userFavourite.ModifiedOn;
                    _userFavourite.IsActive = userFavourite.IsActive;
                    _db.Update<UserFavourite>(_userFavourite);
                }
                else
                {
                    _db.Add<UserFavourite>(userFavourite);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool RemoveFavourite(Guid aspNetUserId, Guid releaseId)
        {
            try
            {
                UserFavourite entityToRemove = _db.Query<UserFavourite>().Where(u => u.AspNetUserId == aspNetUserId && u.ReleaseId == releaseId).FirstOrDefault();
                if (entityToRemove == null)
                    return false;
                else
                {
                    _db.Remove<UserFavourite>(entityToRemove);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
