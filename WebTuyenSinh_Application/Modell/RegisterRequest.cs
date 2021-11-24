using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTuyenSinh_Application.Modell
{
    public class RegisterRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a valid Password")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a valid Password")]
        public string Password { get; set; }
        [Compare(otherProperty: "Password", ErrorMessage = "Password & confirm password does not match")]
        public string ConfirmPassword { get; set; }
        [StringLength(200)]
        public string UseName { get; set; }
        [StringLength(200)]
        public string LastName { get; set; }

        [StringLength(200)]
        public string FirtName { get; set; }

        [StringLength(200)]
        public string Adress { get; set; }

        [StringLength(11)]
        public string Telephone { get; set; }
        public DateTime? CreateDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a valid Birthday")]
        public DateTime? BrithDay { get; set; }
    }
}
