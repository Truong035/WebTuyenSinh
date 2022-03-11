using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.Interface;
using System.Linq;
using WebTuyenSinh_Application.ViewApi;
using Microsoft.EntityFrameworkCore;
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
                        major.Description = item.Description;
                        major.delete = false;
                       await _context.SaveChangesAsync();
                    }

                }
                else { majors.Add(item); }

            }
            await _context.Majors.AddRangeAsync(majors);
            await _context.SaveChangesAsync();
            return new ApiResult() { Success = true, Message = "Thành công", Data = null };
        }

        public async Task<ApiResult> Delete(string id)
        {
            var major = await _context.Majors.FindAsync(id);
            if (major == null) return new ApiResult() { Data = null, Message = "Không tìm thấy", Success = false };

            try {

                major.delete = true;
                 _context.SaveChanges();
            return  new ApiResult() { Data = major, Message = "Xóa thành công", Success = true };

            }
            catch { return new ApiResult() { Data = null, Message = "Lỗi Server", Success = false }; }
          
        }

        public  async Task<List<Major>> GetAll()
        {
            
          return await  _context.Majors.Where(x => x.delete == false || x.delete == null).ToListAsync();
        }

        public async  Task<ApiResult> Update(string id, Major request)
        {
            var major = await _context.Majors.FindAsync(id);
            if (major == null) return new ApiResult() { Data = null, Message = "Không tìm thấy", Success = false };

            try
            {
                major.Name = request.Name;
                major.Description = request.Description;
                major.delete = true;
                _context.SaveChanges();
                return new ApiResult() { Data = major, Message = "Cập nhật thành công", Success = true };

            }
            catch { return new ApiResult() { Data = null, Message = "Lỗi Server", Success = false }; }

        }
    }
}
