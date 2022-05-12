using Ecommerce_API.Helper;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.Models
{
    public class ApplicationUser:IdentityUser
    {
        public CustomAddress Address { get; set; } = new CustomAddress();
        public List<CustomMobile> Mobiles { get; set; } = new List<CustomMobile>();
        public string Day { get; set; }
        public string SpecfiyDay { get; set; }
    }
}

