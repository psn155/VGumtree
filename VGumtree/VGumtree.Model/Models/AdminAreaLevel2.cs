using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGumtree.Model
{
    /// <summary>
    /// Class represents District or Ward or City in Vietnam
    /// When using Google geocode api, this class may include administrative_area_level_2 and/or locality
    /// </summary>
    public class AdminAreaLevel2 : IIDModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AdminAreaLevel1Id { get; set; }

        public virtual AdminAreaLevel1 AdminAreaLevel1 { get; set; }
    }
}
