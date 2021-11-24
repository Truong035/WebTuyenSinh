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
        [StringLength(10)]
        public string? idMajor { get; set; }

        [StringLength(9)]
        public string idBlock { get; set; }

        public long? idProfile { get; set; }

        public bool? Statust { get; set; }
        public int? Year { get; set; }
        public double? subject1 { get; set; }
        public double? subject2 { get; set; }
        public double? subject3 { get; set; }
        public double? subject4 { get; set; }
        public double? subject5 { get; set; }
        public double? subject6 { get; set; }
        public double? subject7 { get; set; }
        public double? subject8 { get; set; }
        public double? subject9 { get; set; }
        public int STT { get; set; }
        public virtual Block Block { get; set; }

        public virtual ProfileStudent ProfileStudent { get; set; }
    }
}
