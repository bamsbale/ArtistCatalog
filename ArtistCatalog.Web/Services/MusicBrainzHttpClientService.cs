using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ArtistCatalog.Web.Services
{
    public class MusicBrainzHttpClientService : IHttpClientService
    {
        public MusicBrainzHttpClientService()
        {
        }

        public object Get(string endpoint,int take = 10)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Sample Dev Test/1.0.0 ( bamsbale@gmail.com )");
                var response = httpClient.GetAsync(endpoint).Result;

                if (!response.IsSuccessStatusCode)
                    return null;

                var result = response.Content.ReadAsStringAsync().Result;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(result);

                var xmlNodeList = xmlDoc.ChildNodes;
                var nodees = new List<XmlNode>(xmlDoc.GetElementsByTagName("release").Cast<XmlNode>());

                var objlist = nodees.Select(x => new
                {
                    releaseId = x.Attributes["id"] != null ? x.Attributes["id"].Value : null,
                    title = x["title"] != null ? x["title"].InnerText : null,
                    status = x["status"] != null ? x["status"].InnerText : null,
                    label = x["label-info-list"] != null ? x["label-info-list"].InnerText : null,
                    numberOfTracks = x["medium-list"]?["track-count"] != null ? x["medium-list"]["track-count"].InnerText : null,
                    yearOfRelease = x["date"] != null ? x["date"].InnerText : null,
                    otherArtists = x["artist-credit"] != null ? x["artist-credit"].GetElementsByTagName("artist").Cast<XmlNode>().Select(y => new
                    {
                        id = y.Attributes["id"] != null ? y.Attributes["id"].Value : null,
                        name = y["name"] != null ? y["name"].InnerText : null,
                    }) : null
                });

                return objlist.Take(take);
            }
        }
    }
}
