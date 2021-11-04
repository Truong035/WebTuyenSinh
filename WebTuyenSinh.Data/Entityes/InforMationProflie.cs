namespace WebTuyenSinh.Data.Entityes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
  

    [Table("InforMationProflie")]
    public partial class InforMationProflie
    {
        public long id { get; set; }

        public DateTime? idMajor { get; set; }

        [StringLength(9)]
        public string idBlock { get; set; }

        public long? idProfile { get; set; }

        public bool? Statust { get; set; }
        public int? Year { get; set; }

        public virtual Block Block { get; set; }

        public virtual ProfileStudent ProfileStudent { get; set; }
    }
}
