using System;
using System.Collections.Generic;
using System.Text;
using WebTuyenSinh.Data.Entityes;

namespace WebTuyenSinh_Application.Modell
{
   public class SChool
    {
        public string NameSchool { get; set; }
        public string idSchool { get; set; }
        public long? id { get; set; }
        public string idDistrict { get; set; }
        public string idConscious { get; set; }
    }
    public class District
    {
        public string NameDistrict { get; set; }
        public string idDistrict { get; set; }
        public string idConscious { get; set; }
    }
    public class Conscious
    {
        public string NameConscious { get; set; }
        public string idConscious { get; set; }
    }
    public class ProfileView
    {
       public List<Conscious> Conscious { get; set; } = new List<Conscious>();
        public List<District> District { get; set; } = new List<District>();
        public List<SChool> SChool { get; set; } = new List<SChool>();
        public ProfileStudent Data { get; set; }
        public object Majors { get; set; }
        public int CountMajo { get; set; }

    }
    public class InforProfileView
    {

        public List<Major> Majors { get; set; } = new List<Major>();
        public List<InforMationProflie> InforMationProflie { get; set; } = new List<InforMationProflie>();
        public object Data { get; set; }
    }
}
