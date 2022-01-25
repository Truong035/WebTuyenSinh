using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebTuyenSinh.Data.Entityes
{
    [Table("Role")]
    public class Role
    {

        public long id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
        public IEnumerable<User> Users { get; set; } = new List<User>();
        public IEnumerable<Role_Permisstion> Role_Permisstion { get; set; } = new List<Role_Permisstion>();
    }
}
