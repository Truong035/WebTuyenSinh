namespace WebTuyenSinh.Data.Entityes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("School")]
    public partial class School
    {
        public long id { get; set; }
        [StringLength(500)]
        public string NameConscious { get; set; }
        [StringLength(12)]
        public string idConscious { get; set; }
        [StringLength(500)]
        public string NameDistrict { get; set; }
        [StringLength(12)]
        public string idDistrict { get; set; }
        [StringLength(12)]
        public string idShool { get; set; }

        [StringLength(500)]
        public string NameShool { get; set; }

        [StringLength(500)]
        public string Adrees { get; set; }
        [StringLength(500)]
        public string Area { get; set; }


    }
}
