using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebTuyenSinhClient.Models
{
    public class CustomPasswordPolicy: PasswordValidator<AppUser>
    {
    }
}
