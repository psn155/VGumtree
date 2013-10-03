using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGumtree.Model
{
    public class Country : IIDModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }    // TO DO: Create index for this column

        public virtual ICollection<AdminAreaLevel1> AdminAreaLevel1s { get; set; }
    }
}
