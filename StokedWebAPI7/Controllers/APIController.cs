using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using StokedWebAPI7.Repository;
using StokedWebAPI7.Models;

namespace StokedWebAPI7.Controllers
{
    public class APIController : ApiController
    {

        //This class is the API controller, that returns a list of locations from the database.
        //It will call the GetAll() from LocationRepository and return the variable as a JSON file. 
        private LocationRepository locationRepository;
        
        public APIController()
        {
            locationRepository = new LocationRepository();
        }

        //We dont need to specify that the method should return the items in the list as a JSON file as this is happening automatically. 
        public IEnumerable<LocationModel> Get()
        {
            // retrive the data from table  
            var locationlist = locationRepository.GetAll();

            return locationlist;

        }


    }


}