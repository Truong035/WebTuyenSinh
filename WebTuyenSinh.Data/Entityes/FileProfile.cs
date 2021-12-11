using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebTuyenSinh.Data.Entityes
{
    public partial  class FileProfile
    {
        public long id { get; set; }

        [StringLength(100)]
        public string url { get; set; }

        [StringLength(1)]
        public string Name { get; set; }

        public long? idProfile { get; set; }

        public virtual ProfileStudent ProfileStudent { get; set; }
    }
}
