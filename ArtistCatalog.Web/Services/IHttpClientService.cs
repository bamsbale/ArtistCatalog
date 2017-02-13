using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtistCatalog.Web.Services
{
    public interface IHttpClientService
    {
        object Get(string endpoint,int take);
    }
}
