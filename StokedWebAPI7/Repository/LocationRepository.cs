using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StokedWebAPI7.Repository;

using StokedWebAPI7.Models;

namespace StokedWebAPI7.Repository
{
    public class LocationRepository : iLocationRepository
    {
        
        ApplicationDbContext db = new ApplicationDbContext();

        public void DeleteLocation(int Id)
        {
            LocationModel lm = db.LocationModels.Find(Id);
            db.LocationModels.Remove(lm);
            db.SaveChanges();
        }

        public LocationModel Find(int Id)
        {
            LocationModel lm = db.LocationModels.Find(Id);
            return lm;
        }

        public IEnumerable<LocationModel> GetAll()
        {
            return db.LocationModels.ToList();
        }

        public void InsertOrUpdate(String CurrentUserId, LocationModel location)
        {

            if (string.IsNullOrEmpty(location.FullName))
            {

                location.AspUserId = CurrentUserId;

                ApplicationUser CurrentUser = db.Users.FirstOrDefault(x => x.Id == CurrentUserId);
                location.FullName = CurrentUser.FirstName + " " + CurrentUser.LastName;
            }
            if (location.LocationId <= default(int))
            {

                db.LocationModels.Add(location);

            }
            else
            {
                db.Entry(location).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
        }

        public void insertOrUpdate(String currentUserId, LocationModel locations)
        {

            if (string.IsNullOrEmpty(locations.FullName))
            {

                locations.AspUserId = currentUserId;

                ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
                locations.FullName = currentUser.FirstName + " " + currentUser.LastName;
            }
            if (locations.LocationId <= default(int))
            {

                db.LocationModels.Add(locations);

            }
            else
            {
                db.Entry(locations).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
        }
    }
}