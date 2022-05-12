using Ecommerce_API.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_API.DTO
{
    public class RegisterDto
    {
        [Required]
        public string UserName { set; get; }
        [Required]
        public string Email { set; get; }
        [Required]
        public CustomAddress Address { set; get; } = new CustomAddress();
        [Required]
        public List<string> PhoneNumber { set; get; } = new List<string>();
        [Required, DataType(DataType.Password)]
        public string Password { set; get; }
        public string Day { get; set; }
        public string SpecfiyDay { get; set; }
    }
}
