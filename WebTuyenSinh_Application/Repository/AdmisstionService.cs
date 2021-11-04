using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.Modell;
using WebTuyenSinh_Application.ViewApi;

using WebTuyenSinh.Data.Entityes;

namespace WebTuyenSinh_Application.Repository
{
    public class AdmisstionData
    {
        public List<Major> Majors { get; set; } = new List<Major>();
        public List<Block> Blocks { get; set; } = new List<Block>();
    }
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
                admisstion.Statust = request.Statust;
                admisstion.Type = request.Type;
                await _context.Admisstions.AddAsync(admisstion);
                var id = await _context.SaveChangesAsync();
                return await CreateAdmisstion_Major(request.Majors, admisstion.id);

          
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

        public async Task<ApiResult> CreateListBlock(List<Block> request)
        {
            List<Block> blocks = new List<Block>();
            foreach (var item in request)
            {
                var block = await _context.Blocks.FindAsync(item.id);
                if ( block != null)
                {
                    if (block.Desscription != item.Desscription || block.Delete != false  )
                    {
                        block.Desscription = item.Desscription;
                        block.Delete = false;
                        await _context.SaveChangesAsync();
                    }

                }
                else { blocks.Add(item); }

            }
            await _context.Blocks.AddRangeAsync(blocks);
            await _context.SaveChangesAsync();
            return new ApiResult() { Success = true, Message = "Success", Data = null };
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
                admisstion.Statust = false;
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
              
              List<Admisstion> admisstions = await _context.Admisstions.Where(x => x.Delete == false).Include(x=>x.Admisstion_Major).ToListAsync();
                return new ApiResult() { Success = true, Message = "Thành Công ", Data = admisstions };
            }
            catch (Exception E)
            {
                List<Admisstion> admisstions = await _context.Admisstions.Where(x => x.Delete == false).ToListAsync();
                return new ApiResult() { Success = true, Message ="", Data = admisstions };
            }
        }

        public async Task<ApiResult> GetBockAll()
        {
            try
            {
                List<Block> admisstions = _context.Blocks.Where(x => x.Delete != true).ToList();
                return new ApiResult() { Success = true, Message = "Thành Công ", Data = admisstions };
            }
            catch (Exception E)
            {
              
                return new ApiResult() { Success = false, Message = ""+E.Message, Data = null };
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
                admisstion.Admisstion_Major = await _context.Admisstion_Major.Where(x => x.Delete != true && x.idAdmisstion==admisstion.id
                ).Include(x => x.Addmisstion_Major_Block).ToListAsync();
                admisstion.Admisstion_Major.First();               
                return new ApiResult() { Success = true, Message = "Thành Công ", Data = admisstion };
            }
            catch (Exception E)
            {
              
                return new ApiResult() { Success = true, Message = "Lỗi server " + E.Message, Data = null };
            }
        }

        public  async Task<ApiResult> GetData()
        {
            try
            {
                var major = await _context.Majors.Where(x => x.delete != true).ToListAsync();
                var Block = await _context.Blocks.Where(x => x.Delete != true).ToListAsync();
                
                return new ApiResult() { Success = true, Message = "Thành Công ", Data = new AdmisstionData { Majors=major,Blocks=Block} };
            }
            catch (Exception e) {
                return new ApiResult() { Success = false, Message = "Lỗi Server "+e.Message, Data = null };
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
                admisstion.Statust = request.Statust;
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
