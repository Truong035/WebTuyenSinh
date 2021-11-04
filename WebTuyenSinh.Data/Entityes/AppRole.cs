using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebTuyenSinh.Data.Entityes
{
    public class AppRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
