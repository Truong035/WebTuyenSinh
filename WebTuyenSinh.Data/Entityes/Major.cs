namespace WebTuyenSinh.Data.Entityes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Major")]
    public partial class Major
    {
        public Major()
        {
            Admisstion_Major = new HashSet<Admisstion_Major>();
        }

        [StringLength(10)]
        public string id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? delete { get; set; }

        public int? Year { get; set; }

        public string Detail { get; set; }

        [StringLength(70)]
        public string imgMajor { get; set; }

        public virtual ICollection<Admisstion_Major> Admisstion_Major { get; set; }
    }
}
