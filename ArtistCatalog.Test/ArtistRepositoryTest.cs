using ArtistCatalog.DAL.Entities;
using ArtistCatalog.Test.Fakes;
using ArtistCatalog.Web.Controllers;
using ArtistCatalog.Web.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ArtistCatalog.Test
{
    [TestClass]
    public class ArtistRepositoryTest
    {
        [TestMethod]
        public void GetArtists()
        {
            // Arrange 
            var db = new FakeArtistCatalogDbContext();
            db.AddSet(TestData.Artists);
            var repository = new FakeArtistCatalogRepository(db);

            // Act
            var artists = repository.GetArtists().ToList();

            //Assert
            Assert.AreEqual(true , artists.ToList().Count == 10);
        }

        [TestMethod]
        public void GetArtistsById()
        {
            // Arrange 
            var db = new FakeArtistCatalogDbContext();
            db.AddSet(TestData.Artists);
            var repository = new FakeArtistCatalogRepository(db);

            // Act
            Guid ArtistId = Guid.NewGuid();
            var artist = repository.GetArtist(ArtistId);

            //Assert
            Assert.AreEqual(true, artist != null);
        }
    }
}
