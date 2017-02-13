using ArtistCatalog.DAL;
using ArtistCatalog.DAL.Entities;
using ArtistCatalog.Web.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ArtistCatalog.Web.Controllers
{
    [RoutePrefix("artist")]
    public class ArtistController : BaseApiController
    {
        IHttpClientService _httpClient;
        public ArtistController(IArtistCatalogRepository repository, IHttpClientService httpClient) : base(repository)
        {
            this._httpClient = httpClient;
        }

        const int PAGE_SIZE = 10;
        const int DEFAULT_PAGE_NUMBER = 0;



        // GET: /artist/search/{search_criteria}/{page_number}/{page_size}
        [HttpGet]
        [Route("search/{search_criteria?}/{page_number?}/{page_size?}", Name = "ArtistSearch")]
        public HttpResponseMessage GetArtists(string search_criteria = "", int? page_number = DEFAULT_PAGE_NUMBER, int? page_size = PAGE_SIZE)
        {
            if (page_number < 0 || page_size < 0)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Request");

            if (search_criteria == null)
                search_criteria = string.Empty;

            IQueryable<Artist> query = Repository.GetArtists();

            var baseQuery = query.Where(a => a.Name.Trim().ToLower().StartsWith(search_criteria.Trim().ToLower()) || a.Aliases.Trim().ToLower().StartsWith(search_criteria.Trim().ToLower()) || search_criteria.Trim() == string.Empty)
                            .OrderBy(a => a.Name);

            var totalCount = baseQuery.Count();
            var totalPages = Math.Ceiling((double)totalCount / (double)page_size);

            var finalQuery = baseQuery.Skip((int)page_size * (int)page_number)
                            .Take((int)page_size)
                            .ToList()
                            .Select(a => ModelFactory.Create(a));

            var result = new
            {
                Results = finalQuery,
                NumberOfSearchResults = totalCount,
                Page = page_number,
                PageSize = page_size,
                NumberOfPages = totalPages
            };

            if (page_number > totalPages - 1 && totalPages != 0)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Request");
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // GET: /artist/<artist_id>/releases
        [HttpGet]
        [Route("{artist_id}/releases", Name = "ArtistReleases")]
        public HttpResponseMessage GetReleasesByArtist(string artist_id)
        {
            Guid tmp;
            if (!Guid.TryParse(artist_id, out tmp))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Request");

            try
            {
                string Url = string.Format("http://musicbrainz.org/ws/2/release/?query=arid:{0}", artist_id);
                var result = _httpClient.Get(Url, PAGE_SIZE);

                if (result == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { releases = result } );
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something went wrong");
            }
        }
    }
}