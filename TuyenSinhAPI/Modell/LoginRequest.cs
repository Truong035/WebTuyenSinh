using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuyenSinhAPI.Modell
{
    public class LoginRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a valid Email")]
        //    [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a valid Password")]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
