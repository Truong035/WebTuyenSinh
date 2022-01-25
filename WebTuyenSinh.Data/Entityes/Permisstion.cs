using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebTuyenSinh.Data.Entityes
{
    [Table("Permisstion")]
  public  class Permisstion
    {
        public long id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
        public IEnumerable<Role_Permisstion> Role_Permisstion { get; set; } = new List<Role_Permisstion>();
    }
}
