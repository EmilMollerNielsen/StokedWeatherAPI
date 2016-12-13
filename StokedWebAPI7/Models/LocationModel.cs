using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StokedWebAPI7.Models
{
    //This is our location class.
    public class LocationModel
    {
        //Location ID
        [Key]
        public int LocationId { get; set; }

        //Location name
        public string LocationName { get; set; }

        //Location latitude
        public double LocationLat { get; set; }

        //Location longitude
        public double LocationLong { get; set; }

        //Location description
        [DataType(DataType.MultilineText)]
        public string LocationDescription { get; set; }

        //Type of sports at location
        public bool IsSurfLocation { get; set; }
        public bool IsSUPLocation { get; set; }
        public bool IsWhiteWaterLocation { get; set; }

        //Show location on map
        public bool showLocation { get; set; }

        //Creation date
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime CreationDate { get; set; }

        //Difficulty
        
        public int SurfDifficulty { get; set; }
        
        public int SUPDifficulty { get; set; }
       
        public int WhiteWaterDifficulty { get; set; }

        //Imagedata
        public byte[] ImageData { get; set; }

        //Best surf conditions
        public double BestWindSpeed { get; set; }
        public string BestWindDirection { get; set; }
        public double BestWaveHeight { get; set; }
        public string BestWaveDirection { get; set; }

        //Created by
        public string AspUserId { get; set; }

        public string FullName { get; set; }


    }
}