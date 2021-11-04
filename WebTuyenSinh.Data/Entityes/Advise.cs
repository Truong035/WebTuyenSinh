namespace WebTuyenSinh.Data.Entityes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Advise")]
    public partial class Advise
    {
        public long id { get; set; }

        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(11)]
        public string Telephone { get; set; }

        public DateTime? CreateDate { get; set; }

        public string Message { get; set; }
    }
}
