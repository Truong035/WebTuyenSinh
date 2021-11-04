namespace WebTuyenSinh.Data.Entityes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Catergory")]
    public partial class Catergory
    {
       
        public Catergory()
        {
            CatergoryDetails = new HashSet<CatergoryDetail>();
        }

        public long id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? Delete { get; set; }

   
        public virtual ICollection<CatergoryDetail> CatergoryDetails { get; set; }
    }
}
