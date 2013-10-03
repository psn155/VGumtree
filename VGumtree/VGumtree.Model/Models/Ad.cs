using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGumtree.Model
{
    public class Ad : IIDModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(1024)]
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime? InactiveDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Condition { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }

        [Range(1, 100000000)]
        public double Price { get; set; }

        public int SubCategoryId { get; set; }
        public int UserId { get; set; }
        
        public virtual SubCategory SubCategory { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<AdAttribute> AdAttributes { get; set; }
    }
}
