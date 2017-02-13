using ArtistCatalog.DAL;
using ArtistCatalog.DAL.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArtistCatalog.Web.Controllers
{
    public class HomeController : Controller
    {
        IArtistCatalogRepository _repository;

        public HomeController(IArtistCatalogRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult MyFavourites()
        {
            Guid UserId = new Guid(User.Identity.GetUserId());
            IEnumerable<UserFavourite> ufList = _repository.GetUserFavourites(UserId, true);

            ViewBag.Message = "Your application description page.";

            return View(ufList.OrderByDescending(u => u.ModifiedOn));
        }

        public ActionResult Delete(string releaseId)
        {
            Guid ReleaseId;
            Guid.TryParse(releaseId, out ReleaseId);

            Guid UserId = new Guid(User.Identity.GetUserId());
            _repository.RemoveFavourite(UserId,ReleaseId);
            _repository.SaveAll();

            return RedirectToAction("MyFavourites");
        }
    }
}