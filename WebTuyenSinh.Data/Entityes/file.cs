namespace WebTuyenSinh.Data.Entityes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    public partial class file
    {
        public long id { get; set; }

        [StringLength(100)]
        public string url { get; set; }

        [StringLength(1)]
        public string Name { get; set; }

        public long? idPost { get; set; }

        public virtual Post Post { get; set; }
    }
}
