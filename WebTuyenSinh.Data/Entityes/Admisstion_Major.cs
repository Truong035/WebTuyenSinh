namespace WebTuyenSinh.Data.Entityes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class Admisstion_Major
    {
       
        public Admisstion_Major()
        {
            Addmisstion_Major_Block = new HashSet<Addmisstion_Major_Block>();
    
        }

        public long id { get; set; }

        [StringLength(10)]
        public string idMajor { get; set; }

        public long? idAdmisstion { get; set; }

        public int? Quantity { get; set; }
        public int? Count { get; set; }
        
        public bool? Delete { get; set; }

        public DateTime? OpenTime { get; set; }

        public DateTime? CloseTime { get; set; }

        public bool? Statust { get; set; }
        public virtual ICollection<Addmisstion_Major_Block> Addmisstion_Major_Block { get; set; }

        public virtual Admisstion Admisstion { get; set; }

        public virtual Major Major { get; set; }

    }
}
