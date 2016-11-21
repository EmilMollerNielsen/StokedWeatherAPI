using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using StokedWebAPI7.Models;
using StokedWebAPI7.Repository;
using Microsoft.AspNet.Identity;
using System.IO;
using System;
using System.Web.Hosting;

namespace StokedAPI6.Controllers
{
    [Authorize]
    public class LocationModelsController : Controller
    {

        private const string URL = "http://api.worldweatheronline.com/premium/v1/marine.ashx";
        private string key = "?key=c61de12a0f854c92bc0102010160211&q=";
        private string format = "&format=JSON";
        private string timeInterval = "&tp=24";
        private string result;


        private iLocationRepository locationRepository;

       
        public LocationModelsController(iLocationRepository locationRepo)
        {
            locationRepository = locationRepo;
        }


        //UDPATE WEATHER
        public void Get()
        {
            //var client = new WebClient();
            //ID bruges ikke
            int tempID = -1;
            List<LocationModel> locationList = locationRepository.GetAll() as List<LocationModel>;

            foreach (LocationModel currentLocation in locationList)
            {
                var client = new WebClient();
                //ID bruges ikke
                tempID = currentLocation.LocationId;

                string coord = currentLocation.LocationLat + "," + currentLocation.LocationLong;

                //result = client.DownloadString("http://api.worldweatheronline.com/premium/v1/marine.ashx?key=c61de12a0f854c92bc0102010160211&q=55.12,12.45&format=JSON&tp=24");
                result = client.DownloadString(URL + key + coord + format + timeInterval);

                System.Diagnostics.Debug.WriteLine("Here's the result");
                System.Diagnostics.Debug.WriteLine(result);

                string fileName = currentLocation.LocationId + ".txt";
                string path = HostingEnvironment.MapPath(@"~/WeatherJson/" + currentLocation.LocationId + ".txt");

                if (!System.IO.File.Exists(path))
                {
                    System.Diagnostics.Debug.WriteLine("File doesnt exists, attempting to create...");
                    
                    System.IO.File.Create(path).Close();
                    
                    System.IO.File.WriteAllText(path, result);
                    
                    System.Diagnostics.Debug.WriteLine("File with name " + currentLocation.LocationId + ".txt created!");
                    
                    
                }
                else if (System.IO.File.Exists(path))
                {
                    System.Diagnostics.Debug.WriteLine("File exists, overwriting!");
                        //TextWriter tw = new StreamWriter(path, false);
                        //tw.WriteLine(result);
                        //tw.Close();
                        //System.Diagnostics.Debug.WriteLine("Overwritten file with name "+currentLocation.LocationId+".txt!");

                    System.IO.File.WriteAllText(path, result);

                }

                //System.IO.File.WriteAllText(@".\WeatherJson\" + currentLocation.LocationId+".txt", result);
            }


        }
        //UPDATE WEATHER


        // GET: LocationModels
        [HttpGet]
        public ActionResult Index()
        {
            List<LocationModel> locations = locationRepository.GetAll() as List<LocationModel>;
            return View(locations);
        }
        





        // GET: LocationModels/Details/5
        public ActionResult Details(int id)
        {
            var location = locationRepository.Find(id);
            return View(location);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        // GET: LocationModels/Create
        [HttpPost]
        public ActionResult Create(LocationModel location)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();

                locationRepository.InsertOrUpdate(currentUserId, location);
                return RedirectToAction("Index");
            }
            return View();
        }


        // GET: LocationModels/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {


            return View(locationRepository.Find(id));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LocationModel location)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();

                locationRepository.InsertOrUpdate(currentUserId, location);
                return RedirectToAction("Index");
            }

            return View();
        }



        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (locationRepository.Find(id) == null)
            {
                return HttpNotFound();
            }
            return View(locationRepository.Find(id));
        }

        // POST: UserModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            locationRepository.DeleteLocation(id);
            return RedirectToAction("Index");
        }
        
        
    }
    
   
}
