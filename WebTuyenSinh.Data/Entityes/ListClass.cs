using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebTuyenSinh.Data.Entityes
{
   public class ListClass
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

        [StringLength(10)]
        public string idShool  { get; set; }
  
        public string Name { get; set; }


    }
}
