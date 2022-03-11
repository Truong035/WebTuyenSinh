using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebTuyenSinh.Data.Entityes
{
    [Table("User")]
    public class User
    {

        public long id { get; set; }

        [StringLength(200)]
        public string UserName { get; set; }

        [StringLength(200)]
        public string Email { get; set; }

        [StringLength(500)]
        public string Pass { get; set; }

        public DateTime? CreateDate { get; set; }
        public long? idRole { get; set; }
        public bool? delete { get; set; }
        public Role Role { get; set; }

    }
}
