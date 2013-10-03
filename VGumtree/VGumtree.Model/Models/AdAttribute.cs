using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGumtree.Model
{
    public class AdAttribute : IIDModel
    {
        public int Id { get; set; }
        public int AdId { get; set; }
        public int AttributeId { get; set; }
        public string Value { get; set; }        

        public virtual Ad Ad { get; set; }
        public virtual Attribute Attribute { get; set; }
    }
}
