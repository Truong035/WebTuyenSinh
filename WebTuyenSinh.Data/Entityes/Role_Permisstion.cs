using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebTuyenSinh.Data.Entityes
{
    [Table("Role_Permisstion")]
    public class Role_Permisstion
    {
        public long id { get; set; }
        public long? idRole { get; set; }
        public long? idPermisstion { get; set; }

        public Permisstion Permisstion { get; set; }
        public Role Role { get; set; }
    }
}
