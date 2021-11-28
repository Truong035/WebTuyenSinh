namespace WebTuyenSinh.Data.Entityes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Block")]
    public partial class Block
    {
     
        public Block()
        {
            Addmisstion_Major_Block = new HashSet<Addmisstion_Major_Block>();
            InforMationProflies = new HashSet<InforMationProflie>();
        }

        [StringLength(9)]
        public string id { get; set; }

        [StringLength(500)]
        public string Desscription { get; set; }

        public bool? Delete { get; set; }

        public virtual ICollection<Addmisstion_Major_Block> Addmisstion_Major_Block { get; set; }

      
        public virtual ICollection<InforMationProflie> InforMationProflies { get; set; }
    }
}
