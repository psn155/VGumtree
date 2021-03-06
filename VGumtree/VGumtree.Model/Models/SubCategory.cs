﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGumtree.Model
{
    public class SubCategory : IIDModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public int CategoryId { get; set; }
        
        //Use Private Set to make XML serialization work without circular reference error
        //For JSON serialization, needs to add extra line in WebApiConfig.cs
        public virtual ICollection<Ad> Ads { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Attribute> Attributes { get; set; }
    }
}
