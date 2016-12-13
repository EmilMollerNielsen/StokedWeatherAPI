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
    //This class looks up the weather JSON data file from a specific location. The iOS application will call this method for each location the user wants to see.
    //Each call returns a JSON format.
    //The JSON file located on the server is already in JSON format
    //As the API controller automacially "JSONifies" the return format we will have to deserialize the original JSON file, before we returns it to the user.
    //We do this to avoid double serialization.
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
