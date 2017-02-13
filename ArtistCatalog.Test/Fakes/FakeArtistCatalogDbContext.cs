using ArtistCatalog.DAL;
using ArtistCatalog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistCatalog.Test.Fakes
{
    class FakeArtistCatalogDbContext : IArtistCatalogDbContext
    {
        public IQueryable<T> Query<T>() where T : class
        {
            return Sets[typeof(T)] as IQueryable<T>;
        }
        public void AddSet<T>(IQueryable<T> objects) where T : class
        {
            Sets.Add(typeof(T), objects);
        }

        public void Add<T>(T entity) where T : class
        {
            Sets.Add(typeof(T), entity);
        }
        public void Update<T>(T entity) where T : class
        {
            Sets[typeof(T)] = entity;
        }
        public void Remove<T>(T entity) where T : class
        {
            Sets.Remove(typeof(T));
        }
        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
        }



        public Dictionary<Type, object> Sets = new Dictionary<Type, object>();
    }
}
