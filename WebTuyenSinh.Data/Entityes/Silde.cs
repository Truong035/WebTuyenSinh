namespace WebTuyenSinh.Data.Entityes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Silde")]
    public partial class Silde
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long id { get; set; }

      
        public DateTime? CreateDate { get; set; }

        public bool? delete { get; set; }

 
        public bool? Status { get; set; }

 
        public DateTime? OpenTime { get; set; }

 
        public DateTime? CloseTime { get; set; }
    }
}
