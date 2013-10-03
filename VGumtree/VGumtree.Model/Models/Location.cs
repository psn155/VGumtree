using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGumtree.Model
{
    /// <summary>
    /// Table to store location address. This table needs to be denormalized in order to store the Latitude, Longtitude as well as DbGeopgraphy data
    /// </summary>
    public class Location : IIDModel
    {
        public int Id { get; set; }
        public string EnteredAddress { get; set; }
        public string FormattedAddress { get; set; }
        public int? AdminAreaLevel2Id { get; set; }
        public DbGeography GeoLocation { get; set; }
        public double? Latitude { get; set; }
        public double? Longtitude { get; set; }        

        public virtual AdminAreaLevel2 AdminAreaLevel2 { get; set; }

        public int AdId { get; set; }

        public virtual Ad Ad { get; set; }
    }
}
