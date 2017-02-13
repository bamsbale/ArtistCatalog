using ArtistCatalog.DAL;
using ArtistCatalog.DAL.Entities;
using ArtistCatalog.Web.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArtistCatalog.Web.Controllers.API
{
    public class ReleaseController : BaseApiController
    {
        IHttpClientService _httpClient;
        public ReleaseController(IArtistCatalogRepository repository, IHttpClientService httpClient) : base(repository)
        {
            this._httpClient = httpClient;
        }



        // GET: /favourites/<release_id>
        [HttpPost]
        [Route("~/favourites/add", Name = "AddUserFavourites")]
        public HttpResponseMessage AddToFavourites(UserFavourite userFavourite)
        {
            if (!User.Identity.IsAuthenticated)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized Request");

            try
            {
                UserFavourite uf = new UserFavourite();
                uf.AspNetUserId = new Guid(User.Identity.GetUserId());
                uf.ReleaseId = userFavourite.ReleaseId;
                uf.Title = userFavourite.Title;
                uf.ReleaseYear = userFavourite.ReleaseYear;
                uf.Label = userFavourite.Label;
                uf.NoOfTracks = userFavourite.NoOfTracks;
                uf.ModifiedOn = DateTime.Now;
                uf.IsActive = userFavourite.IsActive;

                var result = false;
                Repository.AddOrUpdateUserFavourite(uf);
                result = Repository.SaveAll();

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something went wrong");
            }
        }


        // GET: /favourites/<user_id>/<is_active>
        [HttpGet]
        [Route("~/favourites/{user_id?}/{is_active?}", Name = "GetUserFavourites")]
        public HttpResponseMessage GetUserFavourites(string user_id, bool is_active)
        {
            Guid tmp;
            if (!Guid.TryParse(user_id, out tmp))
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Request");

            if (!User.Identity.IsAuthenticated)
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized Request");

            try
            {
                var baseResult = Repository.GetUserFavourites(tmp, is_active);
                var result = baseResult != null ? baseResult.Select(f => f.ReleaseId).ToArray() : null;

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Something went wrong");
            }
        }

    }
}
