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
        private LocationRepository locationRepository;


        public APIController()
        {
            locationRepository = new LocationRepository();
        }

        public IEnumerable<LocationModel> Get()
        {
            // retrive the data from table  
            var locationlist = locationRepository.GetAll();

            return locationlist;

        }


    }


}