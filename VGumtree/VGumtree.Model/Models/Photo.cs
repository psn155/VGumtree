using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGumtree.Model
{
    public class Photo : IIDModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
        public int Order { get; set; }

        public int AdId { get; set; }
    }
}
