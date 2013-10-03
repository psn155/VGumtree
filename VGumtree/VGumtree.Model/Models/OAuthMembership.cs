using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGumtree.Model
{
    [Table("webpages_OAuthMembership")]
    public class OAuthMembership
    {
        [Key, Column(Order=0)]
        [StringLength(30)]
        public string Provider { get; set; }

        [Key, Column(Order = 1)]
        [StringLength(100)]
        [Required]
        public string ProviderUserId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
