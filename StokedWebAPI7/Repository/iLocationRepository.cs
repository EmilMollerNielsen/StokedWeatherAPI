using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StokedWebAPI7.Models;

namespace StokedWebAPI7.Repository
{
    //This is the interface LocationRepository inherits.
    public interface iLocationRepository
    {
        IEnumerable<LocationModel> GetAll();
        LocationModel Find(int Id);
        void DeleteLocation(int Id);
        void InsertOrUpdate(string CurrentUserId, LocationModel location);

    }
}