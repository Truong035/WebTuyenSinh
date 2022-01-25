using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;

namespace WebTuyenSinh_Application.Modell
{
 public  class UserView
    {
        public List<User> User { get; set; } = new List<User>();
        public List<Role> Role { get; set; } = new List<Role>();
    }
}
