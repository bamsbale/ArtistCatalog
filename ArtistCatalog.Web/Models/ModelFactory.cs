using ArtistCatalog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace ArtistCatalog.Web.Models
{
    public class ModelFactory
    {
        private UrlHelper _urlHelper;
        public ModelFactory(HttpRequestMessage req)
        {
            _urlHelper = new UrlHelper(req);
        }

        public ArtistModel Create(Artist artist)
        {
            return new ArtistModel()
            {
                UID = artist.ID,
                Name = artist.Name,
                Country = artist.Country,
                Alias = artist.Aliases.Split(','),
                ReleasesUrl = _urlHelper.Link("ArtistReleases", new { artist_id = artist.ID })
            };
        }
    }
}