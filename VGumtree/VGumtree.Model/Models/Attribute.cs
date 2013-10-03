using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGumtree.Model
{
    public class Attribute : IIDModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int SubCategoryId { get; set; }

        public virtual ICollection<AdAttribute> AdAttributes { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
