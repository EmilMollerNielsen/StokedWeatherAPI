﻿using System.Collections.Generic;
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
    //This is the controller that handles everything related to the LocationModel class.
    //Every DB call is happening in the LocationRepository class.


    [Authorize]
    public class LocationModelsController : Controller
    {
        //Here we are construction parameters for the API call to WorldWeatherOnline.com(WWO) which is where we get our weather data.
        //The key is the API-key we've been given by WWO. This key will remain valid until Jan 01 2017.
        //The WWO API has many different keywords; "q", "format", and "timeInterval".
        //"&q=" can be seen at the end of the key parameter. "q" will always follow the key and consists of the coordinates of the location we wish to see.
        //"format" is related to what format we would like the data in. We would like to use JSON.
        //"timeInterval" is related to how many times a day we would like our weatherdata. e.g. 24 = 1 time a day and 3(which is default by WWO) = 8 times a day (24/8 = 3). 
        //"result" is a string we use to gather the response from WWO.
        private const string URL = "http://api.worldweatheronline.com/premium/v1/marine.ashx";
        private const string key = "?key=c61de12a0f854c92bc0102010160211&q=";
        private const string format = "&format=JSON";
        private const string timeInterval = "&tp=24";
        private string result;


        private iLocationRepository locationRepository;

        public LocationModelsController(iLocationRepository locationRepo)
        {
            locationRepository = locationRepo;
        }

        //This method calls out to WWO to update weather data for the locations in the database.
        //Get() is called with javascript from a button click in the Index view for LocationModels.
        public void Get()
        {
            //Here we are setting the int tempID to -1. 0 is not an invalid location, so we're setting the tempID to -1 to avoid conflicting id's.
            int tempID = -1;
            List<LocationModel> locationList = locationRepository.GetAll() as List<LocationModel>;

            //Here we loop over each object in the locationlist, lookup the coordinates of each object, call WWO with all the parameters and write the returned string to a file.
            //If a file with the same name, the locations ID, already exists, the file will be overwritten with the new data.
            foreach (LocationModel currentLocation in locationList)
            {
                var client = new WebClient();
               
                tempID = currentLocation.LocationId;

                string coord = currentLocation.LocationLat + "," + currentLocation.LocationLong;
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
                   
                    System.IO.File.WriteAllText(path, result);

                }
                
            }

        //The following methods is standard MVC methods, automatically generated by Visual Studio, but changed a bit to outsourcec every db-call to the LocationRepository.
        }
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

                //Here we are setting the date for the newly created location.
                location.CreationDate = DateTime.Now;

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
