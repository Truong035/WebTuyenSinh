using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuyenSinhAPI.Interface;
using TuyenSinhAPI.Modell;
using TuyenSinhAPI.ViewApi;

using WebTuyenSinh.Data.Entityes;

namespace TuyenSinhAPI.Repository
{
    public class AdmisstionService : IAdmisstionService
    {
        private  HeThongTuyenSinhDB _context;

        public AdmisstionService(HeThongTuyenSinhDB context)
        {
            _context = context;
        }
  

        public async Task<ApiResult> CreateAdmisstion(AdmisstionCreate request)
        {
            try
            {
                Admisstion admisstion = new Admisstion();
                admisstion.Name = request.Name;
                admisstion.OpenTime = request.OpenTime;
                admisstion.Quantity = request.Quantity;
                admisstion.CloseTime = request.CloseTime;
                admisstion.Description = request.Description;
                admisstion.Delete = false;
                admisstion.Statust = 0;
                admisstion.Type = request.Type;
                await _context.Admisstions.AddAsync(admisstion);
                var id = await _context.SaveChangesAsync();

                return new ApiResult() { Success = true, Message = "Thành Công", Data = admisstion };
            }
            catch(Exception e)
            {
                return new ApiResult() { Success = false, Message = "Lỗi Server"+e.Message, Data = null };
            }
        }

        public async Task<ApiResult> CreateAdmisstion_Major(List<Admisstion_MajorCreate> request ,long? id )
        {
            try
            {
                if (id==null) { return new ApiResult() { Success = false, Message = "id Không được bỏ trống", Data = null }; }
               
                foreach (Admisstion_MajorCreate item in request)
                {
                    Admisstion_Major admisstion_Major =new Admisstion_Major();
                    admisstion_Major.OpenTime = item.OpenTime;
                    admisstion_Major.CloseTime = item.CloseTime;
                    admisstion_Major.idAdmisstion = id;
                    admisstion_Major.idMajor = item.idMajor;
                    admisstion_Major.Quantity = item.Quantity;
                    await _context.Admisstion_Major.AddAsync(admisstion_Major);
                 await _context.SaveChangesAsync();
                    foreach (var item1 in item.ListBlock)
                    {
                        Addmisstion_Major_Block major_Block = new Addmisstion_Major_Block();
                        major_Block.idAdmisstion = admisstion_Major.id;
                        major_Block.idBlock = item1;
                        admisstion_Major.Addmisstion_Major_Block.Add(major_Block); 
                    }
                    await _context.Addmisstion_Major_Block.AddRangeAsync(admisstion_Major.Addmisstion_Major_Block);
                    await _context.SaveChangesAsync();
                }
                return new ApiResult() { Success = true, Message = "Thành Công", Data = id };
            }
            catch(Exception e)
            {
                return new ApiResult() { Success = false, Message = "Lỗi Server"+e.Message, Data = null };
            }
        }

     
        public async Task<ApiResult> DeleteAdmisstion(long? id)
        {
            if (id == null)
            {
                return new ApiResult() { Success = false, Message = "id không được để trống ", Data = null };
            }

            Admisstion admisstion = await _context.Admisstions.FindAsync(id);

            try {

                if (admisstion == null)
                {
                    return new ApiResult() { Success = false, Message = "Không tìm thấy", Data = null };
                }
                admisstion.Delete = false;
                await _context.SaveChangesAsync();
                _context.Admisstions.Remove(admisstion);
             await   _context.SaveChangesAsync();
                return new ApiResult() { Success = true, Message = " ", Data = admisstion };
            } catch {
                return new ApiResult() { Success = true, Message = " ", Data = admisstion };
            }

        }

        public async Task<ApiResult> DeleteAdmisstion_Major(long? id)
        {
            if (id == null)
            {
                return new ApiResult() { Success = false, Message = "id không được để trống ", Data = null };
            }
            Admisstion_Major admisstion = await _context.Admisstion_Major.FindAsync(id);
            if (admisstion == null)
            {
                return new ApiResult() { Success = false, Message = "Không tìm thấy", Data = null };
            }
            try {
             var block = _context.Addmisstion_Major_Block.Where(x => x.idAdmisstion == admisstion.idAdmisstion).ToList();

                _context.Addmisstion_Major_Block.RemoveRange(block);
                _context.Admisstion_Major.Remove(admisstion);
             await   _context.SaveChangesAsync();
                return new ApiResult() { Success = true, Message = "Thành Công", Data = null };

            } catch (Exception e) {
                return new ApiResult() { Success = false, Message = "Lỗi Server "+e.Message, Data = null };
            }

        }

        public async Task<ApiResult> GetAll()
        {
            try
            {
              
              List<Admisstion> admisstions = await _context.Admisstions.Where(x => x.Delete == false && x.Statust<3).Include(x=>x.Admisstion_Major).ToListAsync();
                return new ApiResult() { Success = true, Message = "Thành Công ", Data = admisstions };
            }
            catch (Exception E)
            {
                List<Admisstion> admisstions = await _context.Admisstions.Where(x => x.Delete == false).ToListAsync();
                return new ApiResult() { Success = true, Message ="", Data = admisstions };
            }
        }

        public async Task<ApiResult> GetByID(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new ApiResult() { Success = false, Message = "id cannot value ", Data = null };
                }
                Admisstion admisstion =await _context.Admisstions.FindAsync(id);
                if (admisstion == null)
                {
                    return new ApiResult() { Success = false, Message = "id Not Found" , Data = null };
                }
                admisstion.Admisstion_Major = await _context.Admisstion_Major.Where(x => x.Delete == false).Include(x => x.Addmisstion_Major_Block).ToListAsync();
               
                return new ApiResult() { Success = true, Message = "Thành Công ", Data = admisstion };
            }
            catch (Exception E)
            {
              
                return new ApiResult() { Success = true, Message = "Lỗi server " + E.Message, Data = null };
            }
        }

        public async Task<ApiResult> UpdateAdmisstion(long? id, AdmisstionCreate request)
        {
            try
            {
                if (id == null)
                {
                    return new ApiResult() { Success = false, Message = "ID không đc bỏ trống ", Data = null };
                }
                Admisstion admisstion = await _context.Admisstions.FindAsync(id);
                if (admisstion == null)
                {
                    return new ApiResult() { Success = false, Message = "Không tìm thấy", Data = null };
                }
                if(request.OpenTime==null|| request.CloseTime == null)
                {
                    return new ApiResult() { Success = false, Message = "Vui lòng nhập Thời gian mở và thời gian đóng", Data = null };
                }
            
                admisstion.Name = request.Name;
                admisstion.OpenTime = request.OpenTime;
                admisstion.Quantity = request.Quantity;
                admisstion.CloseTime = request.CloseTime;
                admisstion.Description = request.Description;

                admisstion.Type = request.Type;
                await _context.Admisstions.AddAsync(admisstion);
           
                return new ApiResult() { Success = true, Message = "Thành công", Data = admisstion };
            }
            catch (Exception e)
            {
                return new ApiResult() { Success = false, Message = "Lỗi server " + e.Message, Data = null };
            }
        }
        public async Task<ApiResult> UpdateStatusAdmisstion_Major(long? id, Admisstion_MajorUpdateStatus updateStatus)
        {
            if (id == null)
            {
                return new ApiResult() { Success = false, Message = "ID không đc bỏ trống ", Data = null };
            }
            Admisstion_Major admisstion = await _context.Admisstion_Major.FindAsync(id);
            if (admisstion == null)
            {
                return new ApiResult() { Success = false, Message = "Không tìm thấy", Data = null };
            }
            try
            {
                admisstion.Statust = updateStatus.Status;
                admisstion.OpenTime = updateStatus.OpenTime;
                admisstion.CloseTime = updateStatus.CloseTime;
                await _context.SaveChangesAsync();
                return new ApiResult() { Success = false, Message = "Thành Công" , Data = admisstion };
            }
            catch (Exception e) {
                return new ApiResult() { Success = false, Message = "Lỗi Server "+e.Message, Data = null };
            }
        }
    }
}
