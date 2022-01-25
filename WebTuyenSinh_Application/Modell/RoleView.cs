using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;

namespace WebTuyenSinh_Application.Modell
{
   public class RoleView
    {
        public List<Permisstion> Permisstion { get; set; } = new List<Permisstion>();
        public List<Role> Role { get; set; } = new List<Role>();
    }
}
