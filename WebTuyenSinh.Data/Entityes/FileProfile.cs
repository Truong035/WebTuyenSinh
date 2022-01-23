using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WebTuyenSinh.Data.Entityes
{
    [Table("FileProfile")]
    public partial  class FileProfile
    {
        public long id { get; set; }

        [StringLength(500)]
        public string url { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public long? idProfile { get; set; }

        public virtual ProfileStudent ProfileStudent { get; set; }
    }
}
