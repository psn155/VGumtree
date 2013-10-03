using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGumtree.Model
{
    /// <summary>
    /// Class represents Province in Vietnam
    /// When using Google geocode api, this class is administrative_area_level_1
    /// </summary>
    public class AdminAreaLevel1 : IIDModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<AdminAreaLevel2> AdminAreaLevel2s { get; set; }
    }
}
