using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.Modell;
using Microsoft.EntityFrameworkCore;
namespace WebTuyenSinh_Application.Repository
{
    public class StatisticalService : IStatisticalService
    {
        private HeThongTuyenSinhDB _context;

        public StatisticalService(HeThongTuyenSinhDB context)
        {
            _context = context;
        }

        public  async Task<Statistical> Statistical(DateTime? fromDate, DateTime? toDate, int? Type, string idMajor)
        {
            var admisstion = await _context.Admisstions.Where(x => x.CreateDate != null && x.Delete != true && x.Statust != 0).ToListAsync();

     

            if (Type != null) admisstion= admisstion.Where(x => x.Type==Type).ToList();
            

            var ProfileInfor = await _context.InforMationProflies.ToListAsync();
            if (idMajor != null && idMajor.Length > 0) {
            
                ProfileInfor = ProfileInfor.Where(x => x.idMajor.Trim().Equals(idMajor.Trim())).ToList();
                    }
            var Profile = await _context.ProfileStudents.ToListAsync();

            if (fromDate != null && toDate != null) Profile = Profile.Where(x => x.CreateDate.Value.Date >= fromDate.Value.Date && x.CreateDate.Value.Date <= toDate.Value.Date && x.Statust>0).ToList();
            var Major = await _context.Majors.ToListAsync();
            var data = (from ad in admisstion
                        join p in Profile on ad.id equals p.idAdmisstion 
                        join PI in ProfileInfor on p.id equals PI.idProfile
                        select new
                        {
                            ad.id,
                            ad.Name,
                            ad.CreateDate
                        }).ToList();
            Statistical statistical = new Statistical();

            var gr = (from c in data
                      group data by new {c.id, c.Name } into g
                      select new
                      {
                          g.Key.Name,
                          value = g.Count(),
                          Bg = new MajorStatistical().Bg,
                          Percent = Math.Round(((double)g.Count() / (double)data.Count) * (double)100, 1),
                      }).ToList();
            statistical.StatisticalAdmisstion = new StatisticalAdmisstion()
            {
                Values = gr.Select(x => x.value).ToList(),
                Name = gr.Select(x => x.Name).ToList(),
                Data= gr
            };
          var   grYear = (from c in data
                      group data by new { c.Name, c.CreateDate.Value.Year } into g
                      select new
                      {
                       name= ""+ g.Key.Year,
                      
                          value = g.Count()
                      }).ToList();
            statistical.StatisticalYear = new StatisticalYear()
            {
                Values = grYear.Select(x => x.value).ToList(),
                Name = grYear.Select(x => x.name).ToList(),
            };

             var dataMajor = (from ad in admisstion
                              join p in Profile on ad.id equals p.idAdmisstion
                              join PI in ProfileInfor on p.id equals PI.idProfile
                              join M in Major on PI.idMajor.Trim() equals M.id.Trim()
                              select new
                        {
                             idMajor=  M.id,
                            M.Name,
                            PI.STT,
                        }).ToList();

            var grMajor = (from c in dataMajor
                           group data by new { c.Name, idMajor } into g
                          select new
                          {
                              name = ""+ g.Key.Name,
                              Bg= new MajorStatistical().Bg,
                              Percent= Math.Round(((double)g.Count() / (double)dataMajor.Count) * (double)100, 1),
                              value = g.Count()
                          }).ToList();
            var grWish = (from c in dataMajor
                           group data by new { c.STT,c.Name } into g
                           select new
                           {
                              g.Key.STT,
                               name = " "+g.Key.Name,
                               value = g.Count()
                           }).ToList();
            statistical.WishOne = new WishOne()
            {
                Values = grWish.Where(x=>x.STT==1).Select(x => x.value).ToList(),
                Name = grWish.Where(x => x.STT == 1).Select(x => x.name).ToList(),
            };
            statistical.WishTwo = new WishTwo()
            {
                Values = grWish.Where(x => x.STT == 2).Select(x => x.value).ToList(),
                Name = grWish.Where(x => x.STT == 2).Select(x => x.name).ToList(),
            };
            statistical.WishThree = new WishThree()
            {
                Values = grWish.Where(x => x.STT == 3).Select(x => x.value).ToList(),
                Name = grWish.Where(x => x.STT == 3).Select(x => x.name).ToList(),
            };
            statistical.WishFor = new WishFor()
            {
                Values = grWish.Where(x => x.STT == 4).Select(x => x.value).ToList(),
                Name = grWish.Where(x => x.STT == 4).Select(x => x.name).ToList(),
            };


            statistical.StatisticalMajor = new StatisticalMajor()
            {
                Values = grMajor.Select(x => x.value).ToList(),
                Name = grMajor.Select(x => x.name).ToList(),
                Data = grMajor
            };
            var grDate = (from c in data
                      group data by new {c.CreateDate} into g
                      select new
                      {
                       name=  g.Key.CreateDate.Value.Day+"/"+g.Key.CreateDate.Value.Month+"/"+ g.Key.CreateDate.Value.Year,
                          value = g.Count()
                      }).ToList();
            statistical.StatisticalDate = new StatisticalDate()
            {
                Values = grDate.Select(x => x.value).ToList(),
                Name = grDate.Select(x => x.name).ToList(),
            };
            var grMonth = (from c in data
                          group data by new { name=c.CreateDate.Value.Month+"-"+ c.CreateDate.Value.Year } into g
                          select new
                          {
                              name = g.Key.name,
                              value = g.Count()
                          }).ToList();
            statistical.StatisticalMonth = new StatisticalMonth()
            {
                Values = grMonth.Select(x => x.value).ToList(),
                Name = grMonth.Select(x => x.name).ToList(),
            };
            var grWishs = (from c in dataMajor
                           group data by new { c.STT } into g
                           select new
                           {
                               name = "NV "+g.Key.STT,
                               value = g.Count()
                           }).ToList();
            statistical.StatisticalWish = new StatisticalWish()
            {
                Values = grWishs.Select(x => x.value).ToList(),
                Name = grWishs.Select(x => x.name).ToList(),
            };
    
            return statistical;
        }

        public async Task<StatisticalHome> StatisticalHome(long? idAdmisstion)
        {
            var admisstion = await _context.Admisstions.Where(x=>x.CreateDate!=null &&x.Delete!=true && x.Statust!=0).OrderByDescending(x=>x.id).ToListAsync();
            var admisstionInfo = await _context.Admisstion_Major.ToListAsync();
            var ProfileInfor = await _context.InforMationProflies.ToListAsync();
            var ProfileStudents = await _context.ProfileStudents.Where(x=>x.Statust!=0 && x.idAdmisstion==idAdmisstion).ToListAsync();
            if (idAdmisstion == null)
            {
                idAdmisstion = (admisstion.FirstOrDefault() != null ? admisstion.FirstOrDefault().id : 0);
            }
            if (idAdmisstion == null || idAdmisstion == 0)
            {
                return new StatisticalHome();

            }

            var Major = await _context.Majors.ToListAsync();
            var data = (
                        from adi in admisstionInfo 
                        join M in Major on adi.idMajor equals M.id
                        join PI in ProfileInfor on M.id.Trim() equals PI.idMajor
                        join PR in ProfileStudents on PI.idProfile equals PR.id
                        where adi.idAdmisstion== idAdmisstion 
                        select new
                        {
                            adi.idMajor,
                            M.Name,
                        }).ToList();
                       ;
            StatisticalHome home = new StatisticalHome();
            home.Message = " " + admisstion.FirstOrDefault(x=>x.id==idAdmisstion)?.Name;
            home.AdmisstionValue = admisstion.Select(x => new AdmissisionValue() {Value=x.id,Name=x.Name}).Distinct().ToList();
            home.TopMajors = (from c in data
                              group data by new { c.idMajor, c.Name} into g
                              select new TopMajor
                              {
                                  id = g.Key.idMajor,
                                  Name = g.Key.Name.Trim(),
                                  NumberProfile = g.Count(),
                                  Number= Math.Round(((double)g.Count() / (double)data.Count) * (double)100,2),
                              }).Take(4).OrderByDescending(x=>x.Number).ToList();
            home.Majors=(from c in data
                        group data by new { c.idMajor, c.Name } into g
                        select new MajorStatistical
                        {
                            id = g.Key.idMajor,
                            Name = g.Key.Name.Trim(),
                            Number = ((double)g.Count()/(double)data.Count)*(double)100,
                            NumberProfile =g.Count()
                        }).ToList();
            return home;
        }
    }
}
