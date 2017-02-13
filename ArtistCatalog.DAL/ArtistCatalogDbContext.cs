using ArtistCatalog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistCatalog.DAL
{
    public interface IArtistCatalogDbContext : IDisposable
    {
        IQueryable<T> Query<T>() where T : class;
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        int SaveChanges();
    }
    public class ArtistCatalogDbContext : DbContext, IArtistCatalogDbContext
    {
        public ArtistCatalogDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<UserFavourite> UserFavourites { get; set; }

        IQueryable<T> IArtistCatalogDbContext.Query<T>()
        {
            return Set<T>();
        }
        void IArtistCatalogDbContext.Add<T>(T entity)
        {
            Set<T>().Add(entity);
        }
        void IArtistCatalogDbContext.Update<T>(T entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
        void IArtistCatalogDbContext.Remove<T>(T entity)
        {
            Set<T>().Remove(entity);
        }

        int IArtistCatalogDbContext.SaveChanges()
        {
            return SaveChanges();
        }
    }
}
