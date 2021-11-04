namespace WebTuyenSinh.Data.Entityes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Table("ProfileStudent")]
    public partial class ProfileStudent
    {
        
        public ProfileStudent()
        {
            InforMationProflies = new HashSet<InforMationProflie>();
        }

        public long id { get; set; }

        public string? idAccount { get; set; }

        [StringLength(70)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Adress { get; set; }

        [StringLength(11)]
        public string Teletephone { get; set; }

        public DateTime? CreateDate { get; set; }

        public long? idAdmisstion { get; set; }

        [StringLength(500)]
        public string Note { get; set; }
        
        public int? Type { get; set; }

        [StringLength(70)]
        public string imgavata { get; set; }

        [StringLength(70)]
        public string url { get; set; }

        public bool? Statust { get; set; }

        [StringLength(12)]
        public string CMND { get; set; }

        public int? Sex { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDay { get; set; }

        [StringLength(200)]
        public string FromBirthDay { get; set; }

        public int? Year { get; set; }
      

        public DateTime? DateRange { get; set; }

        [StringLength(100)]
        public string AdressRange { get; set; }

        [StringLength(50)]
        public string Nation { get; set; }

        public long? Shoo1 { get; set; }
    
        public long? Shoo2 { get; set; }
       
        public long? Shoo3 { get; set; }
        public DateTime? OpenTime { get; set; }
        public DateTime? CloseTime { get; set; }
        public virtual Account Account { get; set; }
        public virtual Admisstion Admisstion { get; set; }
        public virtual ICollection<InforMationProflie> InforMationProflies { get; set; }
  
        public DateTime? Updatedate { get; set; }

    }
}
