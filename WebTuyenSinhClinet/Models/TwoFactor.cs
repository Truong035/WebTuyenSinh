using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTuyenSinhClient.Models
{
    public class TwoFactor
    {
        [Required]
        public string TwoFactorCode { get; set; }
    }
}
