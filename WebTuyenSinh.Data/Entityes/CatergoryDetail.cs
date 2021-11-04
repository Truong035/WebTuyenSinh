namespace WebTuyenSinh.Data.Entityes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public partial class CatergoryDetail
    {
     
        public CatergoryDetail()
        {
            Posts = new HashSet<Post>();
        }

        public long id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int? Quantity { get; set; }

        public bool? Statust { get; set; }

        public bool? Detete { get; set; }

        public long? idCatergory { get; set; }

        public virtual Catergory Catergory { get; set; }


        public virtual ICollection<Post> Posts { get; set; }
    }
}
