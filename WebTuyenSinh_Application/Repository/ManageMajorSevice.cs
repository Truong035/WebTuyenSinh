using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.Interface;
using System.Linq;
using WebTuyenSinh_Application.ViewApi;

namespace WebTuyenSinh_Application.Repository
{
    public class ManageMajorSevice : IManageMajorSevice
    {
        private readonly HeThongTuyenSinhDB _context;

        public ManageMajorSevice(HeThongTuyenSinhDB context)
        {
            _context = context;
        }

        public async Task<ApiResult> CreateList(List<Major> listMajor)
        {
            List<Major> majors = new List<Major>();
            foreach (var item in listMajor)
            {
             var major=   await _context.Majors.FindAsync(item.id);
                if (major != null)
                {
                    if (major.Name != item.Name || major.delete==true)
                    {
                        major.Name = item.Name;
                        major.delete = false;
                       await _context.SaveChangesAsync();
                    }

                }
                else { majors.Add(item); }

            }
            await _context.Majors.AddRangeAsync(majors);
            await _context.SaveChangesAsync();
            return new ApiResult() { Success = true, Message = "Success", Data = null };
        }

        public  async Task<List<Major>> GetAll()
        {
            
          return _context.Majors.Where(x => x.delete == false || x.delete == null).ToList();
        }
    }
}
