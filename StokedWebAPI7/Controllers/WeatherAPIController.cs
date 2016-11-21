using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Hosting;
using System.Web.Http;

namespace StokedWebAPI7.Controllers
{
    public class WeatherAPIController : ApiController
    {
        
        public object Get(int id)
        {
            var fileContents = System.IO.File.ReadAllText(HostingEnvironment.MapPath(@"~/WeatherJson/" + id + ".txt"));

            var cleanedUpFile = JsonConvert.DeserializeObject<object>(fileContents);

            return cleanedUpFile;
        }

    }
}
