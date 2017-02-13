using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArtistCatalog.Web.Controllers.API
{
    [RoutePrefix("security")]
    public class GeneralController : ApiController
    {
        public GeneralController()
        {
        }

        [HttpGet]
        [Route("isauthenticated", Name = "Authentication")]
        public HttpResponseMessage IsAuthenticated()
        {
            var _isAuthenticated = User.Identity.IsAuthenticated;
            var _aspnetuserid = string.Empty;

            if(_isAuthenticated)
                _aspnetuserid = User.Identity.GetUserId();

            var result = new
            {
                IsAuthenticated = _isAuthenticated,
                AspNetUserId = _aspnetuserid
            };

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
