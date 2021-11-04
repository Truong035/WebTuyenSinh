namespace WebTuyenSinh.Data.Entityes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
  

    public partial class Addmisstion_Major_Block
    {
        public long? idAdmisstion { get; set; }

        public long id { get; set; }

        [StringLength(9)]
        public string idBlock { get; set; }

        public virtual Admisstion_Major Admisstion_Major { get; set; }

        public virtual Block Block { get; set; }
    }
}
