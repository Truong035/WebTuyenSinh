namespace WebTuyenSinh.Data.Entityes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
 

    [Table("Admisstion")]
    public partial class Admisstion
    {

        public Admisstion()
        {
            Admisstion_Major = new HashSet<Admisstion_Major>();
            ProfileStudents = new HashSet<ProfileStudent>();
        }

        public long id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? Statust { get; set; }

        public bool? Delete { get; set; }

        public DateTime? OpenTime { get; set; }

        public DateTime? CloseTime { get; set; }

        public int? Quantity { get; set; }
        public int? Type { get; set; }

        public virtual ICollection<Admisstion_Major> Admisstion_Major { get; set; }

  
        public virtual ICollection<ProfileStudent> ProfileStudents { get; set; }
    }
}
