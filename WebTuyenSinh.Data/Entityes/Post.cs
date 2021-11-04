namespace WebTuyenSinh.Data.Entityes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("Post")]
    public partial class Post
    {
        
        public Post()
        {
            files = new HashSet<file>();
        }

        public long id { get; set; }

        public long? idCategory { get; set; }

        public int? view { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(200)]
        public string Category { get; set; }

        [StringLength(10)]
        public string TypePost { get; set; }

        public virtual CatergoryDetail CatergoryDetail { get; set; }


        public virtual ICollection<file> files { get; set; }
    }
}
