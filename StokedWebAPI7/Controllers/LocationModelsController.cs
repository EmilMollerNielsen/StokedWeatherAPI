using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StokedWebAPI7.Models;
using StokedWebAPI7.Repository;
using Microsoft.AspNet.Identity;
using System.Web.Script.Serialization;

namespace StokedAPI6.Controllers
{
    [Authorize]
    public class LocationModelsController : Controller
    {
        private iLocationRepository locationRepository;

        public LocationModelsController(iLocationRepository locationRepo)
        {
            locationRepository = locationRepo;
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
