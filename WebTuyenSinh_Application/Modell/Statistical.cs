using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTuyenSinh_Application.Modell
{
   public class StatisticalHome
    {
        public string Message { get; set; }
        public long StatisticalDate { get; set; }
        public long StatisticalWeek { get; set; }
        public long StatisticalMonth { get; set; }
        public long StatisticalYear { get; set; }
        public List <AdmissisionValue> AdmisstionValue { get; set; }=new List<AdmissisionValue>();
        public List<TopMajor> TopMajors { get; set; } = new List<TopMajor>();
        public List<MajorStatistical> Majors { get; set; } = new List<MajorStatistical>();
    }
    public class AdmissisionValue
    {
        public string Name { get; set; }
        public long Value { get; set; }

    }


    public class Statistical
    {
        public StatisticalAdmisstion StatisticalAdmisstion { get; set; } = new StatisticalAdmisstion();
        public StatisticalYear StatisticalYear { get; set; } = new StatisticalYear();
        public StatisticalMajor StatisticalMajor { get; set; } = new StatisticalMajor();
        public StatisticalDate StatisticalDate { get; set; } = new StatisticalDate();
        public StatisticalMonth StatisticalMonth { get; set; } = new StatisticalMonth();
        public WishOne WishOne { get; set; } = new WishOne();
        public WishTwo WishTwo { get; set; } = new WishTwo();
        public WishThree WishThree { get; set; } = new WishThree();
       public StatisticalWish StatisticalWish { get; set; }= new StatisticalWish();
    }

    public class StatisticalDate
    {
        public List<string> Name { get; set; } = new List<string>();
        public List<int> Values { get; set; } = new List<int>();
    }
    public class StatisticalMonth
    {
        public List<string> Name { get; set; } = new List<string>();
        public List<int> Values { get; set; } = new List<int>();
    }
    public class WishOne
    {
        public List<string> Name { get; set; } = new List<string>();
        public List<int> Values { get; set; } = new List<int>();
    }
    public class WishTwo
    {
        public List<string> Name { get; set; } = new List<string>();
        public List<int> Values { get; set; } = new List<int>();
    }
    public class WishThree
    {
        public List<string> Name { get; set; } = new List<string>();
        public List<int> Values { get; set; } = new List<int>();
    }

    public class StatisticalAdmisstion
    {
        public List<string> Name { get; set; } = new List<string>();
        public List<int> Values { get; set; } = new List<int>();
        public object Data { get; set; }
    }

    public class StatisticalYear
    {
        public List<string> Name { get; set; } = new List<string>();
        public List<int> Values { get; set; } = new List<int>();
    }
    public class StatisticalWish
    {
        public List<string> Name { get; set; } = new List<string>();
        public List<int> Values { get; set; } = new List<int>();
    }
    public class StatisticalMajor
    {
        public List<string> Name { get; set; } = new List<string>();
        public List<int> Values { get; set; } = new List<int>();
        public object Data { get; set; }
    }

    public class TopMajor
    {
        public string id { get; set; }
        public string Name { get; set; }
        public double Number { get; set; }
        public long NumberProfile { get; set; }
        public int Wish { get; set; }
    }
    public class MajorStatistical
    {
        public string id { get; set; }
        public string Name { get; set; }
        public double Number { get; set; }
        public long NumberProfile { get; set; }
        public string Bg { get { return Bgs[new Random().Next(0,Bgs.Count)];} }
        List<string> Bgs = new List<string>() { "bg-primary", "bg-warning", "bg-danger", "bg-info" };

    }
}
