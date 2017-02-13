using ArtistCatalog.DAL;
using ArtistCatalog.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ArtistCatalog.Web.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        IArtistCatalogRepository _repository;
        ModelFactory _modelFactory;

        public BaseApiController(IArtistCatalogRepository repository)
        {
            _repository = repository;
        }

        protected IArtistCatalogRepository Repository
        {
            get
            {
                return _repository;
            }
        }
        protected ModelFactory ModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request);
                }
                return _modelFactory;
            }
        }
    }
}