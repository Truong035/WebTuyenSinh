using System;
using System.Collections.Generic;
using System.Text;

namespace WebTuyenSinh_Application.ViewApi
{
   public class ProfileStudentsView
    {

        public long id { get; set; }

        public string? idAccount { get; set; }
        public int? Quantity { get; set; }

        public string CMND { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }
        public string FromCMND { get; set; }
        public string Adress { get; set; }
        public string Teletephone { get; set; }
        public string Religion { get; set; }
        public DateTime? CreateCMND { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? BirthDay { get; set; }
        public DateTime? DateRange { get; set; }
        public string Priority_object { get; set; }
        public string FromBirthDay { get; set; }
        public string Areas { get; set; }
        public long? idAdmisstion { get; set; }
        public int? Year { get; set; }
        public DateTime? CloseTime { get; set; }
        
 public string Nation { get; set; }
        public string AdressRange { get; set; }
        public string imgavata { get; set; }
  
        public string url { get; set; }
        public string block { get; set; }
        public int? Sex { get; set; }
        public int? Statust { get; set; }
        public string Shoo1 { get; set; }
        public string Shoo2 { get; set; }
        public string Shoo3 { get; set; }
        public string Dictrict1 { get; set; }
        public string Dictrict2 { get; set; }
        public string Dictrict3 { get; set; }
        public string Dictrict4 { get; set; }
        public string Dictrict5 { get; set; }
    }
}
