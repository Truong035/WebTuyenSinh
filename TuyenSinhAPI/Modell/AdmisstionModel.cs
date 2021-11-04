using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TuyenSinhAPI.Modell
{
    public class AdmisstionCreate
    {
        [StringLength(200)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public int? Quantity { get; set; }
        public int? Type { get; set; }
        public bool? Statust { get; set; }
        
    }


  
    public class Admisstion_MajorCreate
    {

        [StringLength(10)]
        public string idMajor { get; set; }
        public int? Quantity { get; set; }
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public List<string> ListBlock { get; set; } = new List<string>();
    }

    public class Admisstion_MajorUpdateStatus
    {
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public bool? Status { get; set; }

    }

    public class AdmisstionUpdate
    {
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public bool? Status { get; set; }

    }




}
