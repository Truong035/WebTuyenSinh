using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.Modell;
using WebTuyenSinh_Application.ViewApi;

using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.System;
using System.IO;
using System.IO.Compression;

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
        private IStorageService _storageService;
        public AdmisstionService(HeThongTuyenSinhDB context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
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
                admisstion.CreateDate = DateTime.Now;
                admisstion.Description = request.Description;
                admisstion.Statust = 0;
                admisstion.Delete = false;
                admisstion.Type = request.Type;
                await _context.Admisstions.AddAsync(admisstion);
                var id = await _context.SaveChangesAsync();
                // If directory does not exist, create it
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admisstion", id.ToString());
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return await CreateAdmisstion_Major(request.Majors, admisstion.id);

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

                for (int i = 0; i < request.Count; i++)
                {
                    for (int j = i+1; j < request.Count; j++)
                    {
                        if (request[i].idMajor.Equals(request[j].idMajor))
                        {
                            request[i].ListBlock.AddRange(request[j].ListBlock);
                            request.RemoveAt(j);
                            i--;
                            break;
                        }
                    }

                }
                foreach (Admisstion_MajorCreate item in request)
                {
                    Admisstion_Major admisstion_Major =new Admisstion_Major();
                    admisstion_Major.OpenTime = item.OpenTime;
                    admisstion_Major.CloseTime = item.CloseTime;
                    admisstion_Major.idAdmisstion = id;
                    admisstion_Major.idMajor = item.idMajor;
                    admisstion_Major.Quantity = item.Quantity;
                    admisstion_Major.Delete = false;
                    await _context.Admisstion_Major.AddAsync(admisstion_Major);
                 await _context.SaveChangesAsync();
                    foreach (var item1 in item.ListBlock)
                    {
                        if (!admisstion_Major.Addmisstion_Major_Block.ToList().Exists(x => x.idBlock.Equals(item1)))
                        {
                            Addmisstion_Major_Block major_Block = new Addmisstion_Major_Block();
                            major_Block.idAdmisstion = admisstion_Major.id;
                            major_Block.idBlock = item1;
                            admisstion_Major.Addmisstion_Major_Block.Add(major_Block);
                        } 
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
                else {
                    item.Desscription = (item.Desscription != null ? item.Desscription : item.id);
                    blocks.Add(item); }

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
                admisstion.Delete = true;
                        await _context.SaveChangesAsync();

                return new ApiResult() { Success = true, Message = "Xóa thành công", Data = admisstion };
            } catch {
                return new ApiResult() { Success = false, Message = "Lỗi server", Data = admisstion };
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
             var block = _context.Addmisstion_Major_Block.Where(x => x.idAdmisstion == admisstion.id).ToList();

                _context.Addmisstion_Major_Block.RemoveRange(block);
                admisstion.Delete = true;
                await _context.SaveChangesAsync();
                _context.Admisstion_Major.Remove(admisstion);
             await   _context.SaveChangesAsync();
                return new ApiResult() { Success = true, Message = "Thành Công", Data = null };

            } catch (Exception e) {
                return new ApiResult() { Success = false, Message = "Lỗi Server "+e.Message, Data = null };
            }

        }

        private async Task  Checkupdate()
        {
            try
            {

                List<Admisstion> admisstions = await _context.Admisstions.Where(x => x.OpenTime <= DateTime.Now && x.Delete != true ).ToListAsync();
                foreach (var item in admisstions)
                {
                    if (item.CloseTime <= DateTime.Now)
                    {
                        item.Statust = 3;
                        await CloseProifile(item.id);
                    }
        
                    else if (item.Statust == 3)
                    {
                        item.Statust = 2;
                       await OpenProFile(item);
                    }
                    else if (item.Statust == 0)
                    {
                        item.Statust = 1;
                    }
                }
                await _context.SaveChangesAsync();
            } catch { }
          await CheckProFile();
        }

        private async Task CloseProifile(long id)
        {
            var Proifile = await _context.ProfileStudents.Where(X => X.idAdmisstion == id).ToListAsync();
            foreach (var item in Proifile)
            {
                item.CloseTime = DateTime.Now;

            }
            await _context.SaveChangesAsync();
        }

        private async Task OpenProFile(Admisstion admisstion)
        {
            var Proifile = await _context.ProfileStudents.Where(X => X.idAdmisstion == admisstion.id).ToListAsync();
            foreach (var item in Proifile)
            {
                item.CloseTime = admisstion.CloseTime;

            }
            await _context.SaveChangesAsync();
        }

        private async Task CheckProFile()
        {
            List<Admisstion> admisstions = await _context.Admisstions.Where(x => x.Statust==2 && x.Delete != true && x.CloseTime.Value<=DateTime.Now).ToListAsync();
            foreach (var item in admisstions)
            {
                item.CloseTime = DateTime.Now;
                item.Statust = 0;
            }
            await _context.SaveChangesAsync();
        }
            public async Task<ApiResult> GetAll(int? status)
        {
            try
            {
                await Checkupdate();
                List<Admisstion> admisstions = await _context.Admisstions.Where(x => x.Delete != true && x.Statust < status && x.Statust!=0).OrderByDescending(x => x.id).ToListAsync();
                if (status == 2)
                {
                    admisstions= admisstions.Where(x => x.Delete != true && x.Statust == 1).OrderByDescending(x => x.id).ToList();
                }
                if (status == 3)
                {
                    admisstions = admisstions.Where(x => x.Delete != true && x.Statust == 2).OrderByDescending(x => x.id).ToList();
                }
                return new ApiResult() { Success = true, Message = "Thành Công ", Data = admisstions };
            }
            catch (Exception E)
            {
                List<Admisstion> admisstions = await _context.Admisstions.Where(x => x.Delete != true && x.Statust < status).OrderByDescending(x => x.id).ToListAsync();
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

           var Admisstion_Major = await _context.Admisstion_Major.Where(x => x.Delete != true && x.idAdmisstion == admisstion.id
                ).ToListAsync();
                var Proifile = await _context.ProfileStudents.Where(x => x.idAdmisstion == id && x.Statust!=0).ToListAsync();
                var Info = await _context.InforMationProflies.ToListAsync();
                var InfoProFile = (from p in Proifile
                                   join i in Info on p.id equals i.idProfile
                                   where p.idAdmisstion==id
                                   select new
                                   {
                                       i.id,
                                       i.idBlock,
                                       i.idMajor,
                                       p.idAdmisstion
                                   }).ToList();
                var Major_Blocl = await _context.Addmisstion_Major_Block.ToListAsync();
                var Major = await _context.Majors.ToListAsync();
                admisstion.Admisstion_Major = (from m in Admisstion_Major
                                                                                                                                  select new Admisstion_Major()
                                               {
                                                   id=m.id,
                                                   CloseTime=m.CloseTime,
                                                   OpenTime=m.CloseTime,
                                                   Count= InfoProFile.Where(x=>x.idMajor.Trim().Equals(m.idMajor.Trim())&& x.idAdmisstion==m.idAdmisstion).ToList().Count,
                                                   Addmisstion_Major_Block= Major_Blocl.Where(x=>x.idAdmisstion==m.id).ToList(),
                                                   idMajor=m.idMajor,
                                                   Quantity=InfoProFile.Where(x=>x.idMajor.Trim()==m.idMajor && x.idBlock.Trim().Equals(m.idMajor)).Count(),
                                                   Statust=m.Statust,
                                                   Delete=m.Delete,
                                                  
                                                   Major=Major.SingleOrDefault(x=>x.id.Trim().Equals(m.idMajor.Trim()))
                                               }

                                              ).ToList();
                        
                return new ApiResult() { Success = true, Message = "Thành Công ", Data = admisstion };
            }
            catch (Exception E)
            {
                return new ApiResult() { Success = false, Message = "Lỗi server " + E.Message, Data = null };
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

        public  async Task<ApiResult> GetResult()
        {
            try
            {
               await Checkupdate();
                List<Admisstion> admisstions = await _context.Admisstions.Where(x => x.Delete != true && x.Statust >= 3).OrderByDescending(x => x.id).Include(x => x.Admisstion_Major).ToListAsync();
                return new ApiResult() { Success = true, Message = "Thành Công ", Data = admisstions };
            }
            catch 
            {
                List<Admisstion> admisstions = await _context.Admisstions.Where(x => x.Delete != true && x.Statust >= 3).OrderByDescending(x => x.id).ToListAsync();
                return new ApiResult() { Success = true, Message = "", Data = admisstions };
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
                for (int i = 0; i < request.Majors.Count; i++)
                {
                    for (int j = i + 1; j < request.Majors.Count; j++)
                    {
                        if (request.Majors[i].idMajor.Equals(request.Majors[j].idMajor))
                        {
                            request.Majors[i].ListBlock.AddRange(request.Majors[j].ListBlock);
                            request.Majors.RemoveAt(j);
                            i--;
                            break;
                        }
                    }

                }

                admisstion.Name = request.Name;
                admisstion.OpenTime = request.OpenTime;
                admisstion.Quantity = request.Quantity;
                if (admisstion.CloseTime != null && admisstion.CloseTime > DateTime.Now)
                {
                    admisstion.CloseTime = request.CloseTime;
                    await OpenProFile(admisstion);
                }
                admisstion.CloseTime = request.CloseTime;
                admisstion.Description = request.Description;
                admisstion.Type = request.Type;
                await _context.SaveChangesAsync();
                if (admisstion.Statust < 2)
                {
                    var admisstion_major = _context.Admisstion_Major.Where(x => x.idAdmisstion == admisstion.id && x.Delete != true).ToList();
                    foreach (var item in admisstion_major)
                    {
                        if (!request.Majors.Exists(x => x.idMajor.Equals(item.idMajor)))
                        {
                            await DeleteAdmisstion_Major(item.id);
                        }
                    }
                    foreach (var item in request.Majors)
                    {
                        try
                        {

                            var majors = _context.Admisstion_Major.Where(x => x.idMajor.Trim().Equals(item.idMajor.Trim()) && x.idAdmisstion == id && x.Delete != true).ToList();
                            if (majors != null && majors.Count > 0)
                            {
                                var major = majors[0];
                                var block = _context.Addmisstion_Major_Block.Where(x => x.idAdmisstion == major.id).ToList();
                                if (block != null)
                                {
                                    _context.Addmisstion_Major_Block.RemoveRange(block);
                                    await _context.SaveChangesAsync();
                                }
                                List<Addmisstion_Major_Block> Addmisstion_Major_Block = new List<Addmisstion_Major_Block>();
                                foreach (var item1 in item.ListBlock)
                                {
                                    if (!Addmisstion_Major_Block.ToList().Exists(x => x.idBlock.Equals(item1)))
                                    {
                                        Addmisstion_Major_Block major_Block = new Addmisstion_Major_Block();
                                        major_Block.idAdmisstion = major.id;
                                        major_Block.idBlock = item1;
                                        Addmisstion_Major_Block.Add(major_Block);
                                    }
                                }
                                await _context.Addmisstion_Major_Block.AddRangeAsync(Addmisstion_Major_Block);
                                await _context.SaveChangesAsync();

                            }
                            else
                            {
                                await CreateAdmisstion_Major(new List<Admisstion_MajorCreate>() { item }, admisstion.id);
                            }
                        }
                        catch (Exception e) { string a = e.Message; }
                    }
                }
              
             
                return new ApiResult() { Success = true, Message = "Thành công", Data = null };
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
    
                admisstion.OpenTime = updateStatus.OpenTime;
                admisstion.CloseTime = updateStatus.CloseTime;
                await _context.SaveChangesAsync();
                return new ApiResult() { Success = true, Message = "Thành Công" , Data = admisstion };
            }
            catch (Exception e) {
                return new ApiResult() { Success = false, Message = "Lỗi Server "+e.Message, Data = null };
            }
        }

        public async Task<ApiResult> DetailAdmisstion(long? id)
        {
           try {
                if (id == null)
                {
                    return new ApiResult() { Success = false, Message = "id cannot value ", Data = null };
                }
                Admisstion admisstion = await _context.Admisstions.FindAsync(id);
                if (admisstion == null)
                {
                    return new ApiResult() { Success = false, Message = "id Not Found", Data = null };
                }
                admisstion.ProfileStudents = await _context.ProfileStudents.Where(x => x.idAdmisstion == admisstion.id
                ).ToListAsync();
                return new ApiResult() { Success = true, Message = "Thành Công ", Data = admisstion };
            }
            catch (Exception E)
            {

                return new ApiResult() { Success = true, Message = "Lỗi server " + E.Message, Data = null };
            }
        }

        public async Task<ApiResult> ListProfile(long? id)
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
            
                var profile = _context.ProfileStudents.Where(x => x.idAdmisstion == admisstion.idAdmisstion && x.Statust!=0).ToList();
                var infor = _context.InforMationProflies.Where(x=>x.idMajor==admisstion.idMajor).ToList();
                List<Addmisstion_Major_Block> Blocks = _context.Addmisstion_Major_Block.Where(x => x.idAdmisstion == admisstion.id).ToList();
                var listinfor = (from s in profile
                                 join i in infor on s.id equals i.idProfile
                                 join b in Blocks on i.idBlock equals b.idBlock
                                 select new ProfileStudentsView
                                 {
                                   id=   s.id,
                                   idAccount=  s.idAccount,
                                   idAdmisstion=  s.idAdmisstion,
                                     CreateDate=   s.CreateDate,
                                    Name=  s.Name,
                                     Email = s.Email,
                                     Teletephone = s.Teletephone,
                                     url = s.url,
                                 block =  i.idBlock,
                                 Statust=s.Statust,
                                     CMND = s.CMND,

                                 }).ToList();
                List<ProfileStudentsView> views = new List<ProfileStudentsView>();
                foreach (var item in listinfor)
                {
                    if (!views.Exists(x => x.id == item.id))
                    {
                        views.Add(item);
                    }
                    else
                    {
                        var a = views.Where(x => x.id == item.id).ToList()[0];
                        a.block += " ," + item.block;
                        views.Where(x => x.id == item.id).ToList()[0] = a;
                    }

                }
           var major= await    _context.Majors.FindAsync(admisstion.idMajor);

                return new ApiResult() { Success = false, Message = ""+major.Name +" ("+major.id+")", Data = views };
            }
            catch (Exception e)
            {
                return new ApiResult() { Success = false, Message = "Lỗi Server " + e.Message, Data = null };
            }
        }

        public async Task<ApiResult> UpdateStatusAdmisstion(long? id, AdmisstionCreate admisstionCreate)
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
                if (admisstionCreate.OpenTime == null || admisstionCreate.CloseTime == null)
                {
                    return new ApiResult() { Success = false, Message = "Vui lòng nhập Thời gian mở và thời gian đóng", Data = null };
                }
                admisstion.Name = admisstionCreate.Name;
                admisstion.OpenTime = admisstionCreate.OpenTime;
                if(admisstion.CloseTime!=null && admisstion.CloseTime > DateTime.Now)
                {
                    admisstion.CloseTime = admisstionCreate.CloseTime;
                   await OpenProFile(admisstion);
                }
                admisstion.CloseTime = admisstionCreate.CloseTime;
                admisstion.Description = admisstionCreate.Description;
                await _context.SaveChangesAsync();
                

               
            }
            catch {   }
            return new ApiResult() { Success = true, Message = "Thành công", Data = null };
        }

        public async Task<ApiResult> UpdateStatusProfile(long? id, string comment, DateTime? date, int? status)
        {
            string mess = "";
            try
            {
                if (id == null)
                {
                    return new ApiResult() { Success = false, Message = "ID không đc bỏ trống ", Data = null };
                }
                ProfileStudent ProfileStudents = await _context.ProfileStudents.FindAsync(id);
                if (ProfileStudents == null)
                {
                    return new ApiResult() { Success = false, Message = "Không tìm thấy", Data = null };
                }
                ProfileStudents.Statust = status;
                ProfileStudents.CloseTime = date;
                ProfileStudents.Note = comment;
                if (status == 1)
                {
                    ProfileStudents.CloseTime = DateTime.Now;
                }
                await _context.SaveChangesAsync();
                if (status == 2 && comment!=null&&comment.Length>0)
                {
                    mess=   new EmailHelper().SendEmail(ProfileStudents.Email, comment, "Thông Báo Từ Đại học giao thông vận tải");
                }
           
            }
            catch { }
            return new ApiResult() { Success = true, Message = "Cập nhật thành công", Data = null };
        }

        public async Task<ApiResult> ListAll(long? id)
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
            try
            {
                var profile = _context.ProfileStudents.Where(x => x.idAdmisstion == id && x.Statust>0).ToList();
             
                var listinfor = (from s in profile
                                 orderby s.CreateDate
                                 select new ProfileStudentsView
                                 {
                                     id = s.id,
                                     idAccount = s.idAccount,
                                     idAdmisstion = s.idAdmisstion,
                                     CreateDate = s.CreateDate,
                                     Name = s.Name,
                                     Email = s.Email,
                                     Teletephone = s.Teletephone,
                                     url = s.url,
                                     CMND = s.CMND,
                                     Statust = s.Statust,
                                     Quantity = s.Quantity

                                 }).ToList();
             
                return new ApiResult() { Success = false, Message = "" + admisstion.Name + " (" + admisstion.id + ")", Data = listinfor };
            }
            catch (Exception e)
            {
                return new ApiResult() { Success = false, Message = "Lỗi Server " + e.Message, Data = null };
            }
        }

        public async Task<ApiResult> GetByProfile(long? id)
        {
            if (id == null)
            {
                return new ApiResult() { Success = false, Message = "ID không đc bỏ trống ", Data = null };
            }
            ProfileStudent student = await _context.ProfileStudents.FindAsync(id);
            if (student == null)
            {
                return new ApiResult() { Success = false, Message = "Không tìm thấy", Data = null };
            }
            student.InforMationProflies = await _context.InforMationProflies.Where(x => x.idProfile == id).ToListAsync();
            student.FileProfiles= await _context.FileProfiles.Where(x => x.idProfile == id).ToListAsync();
            var School = _context.Schools.Select(x => x).ToList();
            var Shool1 = (from c in School
                          select new SChool
                          {
                              id = c.id,
                              NameSchool = c.NameShool,
                              idSchool = c.idShool.Trim()+ "/"+c.idDistrict.Trim() + "/" + c.idConscious.Trim(),
                              idConscious =c.idConscious.Trim(),
                              idDistrict=c.idDistrict.Trim()+"/"+c.idConscious.Trim()

                          });
            var Conscious = (from c in School
                             group School by new { c.idConscious, c.NameConscious } into g
                             select new Conscious
                             {
                                 NameConscious = g.Key.NameConscious,
                                 idConscious = g.Key.idConscious.Trim()

                             }) ;
            var District = (from c in School
                             group School by new { c.idDistrict, c.NameDistrict,c.idConscious } into g
                             select new District
                             {
                                 NameDistrict = g.Key.NameDistrict,
                                 idDistrict = g.Key.idDistrict.Trim()+"/" + g.Key.idConscious.Trim(),
                                 idConscious= g.Key.idConscious.Trim()
                             });
            //var block = _context.Addmisstion_Major_Block.Select(X => new { X.id, X.idBlock, X.idAdmisstion }).ToList();
            var Majors = _context.Majors.Where(x => x.delete != true).Select(X => new { X.id, X.Name }).ToList();
            var Admisstion_Major = await _context.Admisstion_Major.Where(x => x.idAdmisstion == student.idAdmisstion).ToListAsync();
            var MajorsData = (from c in Admisstion_Major

                              join m in Majors on c.idMajor equals m.id

                              select new
                              {
                                  id = m.id,
                                  Name = m.Name,
                              }).ToList();

            ProfileView view = new ProfileView();
            view.Conscious = Conscious.ToList();
            view.District = District.ToList();
            view.SChool = Shool1.ToList();
            view.Majors = MajorsData;
            view.Data = student;

            return new ApiResult() { Success = true, Message = "Không tìm thấy", Data = view };
      
        }

        public async Task<ApiResult> GetInfoProfile(long? id)
        {
            if (id == null)
            {
                return new ApiResult() { Success = false, Message = "ID không đc bỏ trống ", Data = null };
            }
            ProfileStudent student = await _context.ProfileStudents.FindAsync(id);
            if (student == null)
            {
                return new ApiResult() { Success = false, Message = "Không tìm thấy", Data = null };
            }
            student.InforMationProflies = await _context.InforMationProflies.Where(x => x.idProfile == id).ToListAsync();
            var School = _context.Schools.Select(x => x).ToList();
            var Conscious = (from c in School
                             group School by new { c.idConscious, c.NameConscious } into g
                             select new Conscious
                             {
                                 NameConscious = g.Key.NameConscious,
                                 idConscious = g.Key.idConscious

                             });
            var District = (from c in School
                            group School by new { c.idDistrict, c.NameDistrict } into g
                            select new District
                            {
                                NameDistrict = g.Key.NameDistrict,
                                idDistrict = g.Key.idDistrict

                            });
            var Shool1 = (from c in School
                          select new SChool
                          {
                              id = c.id,
                              NameSchool = c.NameShool,
                              idSchool = c.idShool,

                          });
            var block = _context.Addmisstion_Major_Block.Select(X => new { X.id, X.idBlock, X.idAdmisstion }).ToList();
            var Majors = _context.Majors.Where(x => x.delete != true).Select(X => new { X.id, X.Name }).ToList();
            var Admisstion_Major = await _context.Admisstion_Major.Where(x => x.idAdmisstion == id).ToListAsync();

            var MajorsData = (from c in Admisstion_Major
                       
                         join m in Majors on c.idMajor equals m.id
                       
                         select new
                         {
                             id = m.id,
                             Name = m.Name,
                         }).ToList();
            ProfileView view = new ProfileView();
            view.Conscious = Conscious.ToList();
            view.District = District.ToList();
            view.SChool = Shool1.ToList();
            view.Majors = MajorsData;
            view.Data = student;

            return new ApiResult() { Success = true, Message = "Không tìm thấy", Data = view };
        }

        public async Task<ApiResult> CreateProfile(ProfileStudent profile,List<FileProfileView> files)
        {
            ProfileStudent profileStudent = new ProfileStudent();

            Admisstion admisstion = _context.Admisstions.Where(x => x.id == profile.idAdmisstion).FirstOrDefault();


            if (admisstion == null)
            {
                return new ApiResult() { Success = false, Message = "Không Tìm Thấy Đợt Tuyển Sinh", Data = profileStudent };
            }

            try {
            List<InforMationProflie> inforMations = new List<InforMationProflie>();
            foreach (var item in profile.InforMationProflies)
            {
                if (!inforMations.Exists(x => x.idMajor.Trim().Equals(item.idMajor.Trim()) && (admisstion.Type==1 ||x.idBlock.Trim().Equals(item.idBlock.Trim()))))
                { 
                    inforMations.Add(new InforMationProflie() { 
                    idBlock=item.idBlock,
                    idMajor=item.idMajor,
                        subject1 = (item.subject1!=null? Math.Round(item.subject1.Value, 2) : 0) ,
                        subject2 = (item.subject2 != null ? Math.Round(item.subject2.Value, 2) : 0),
                        subject3 = (item.subject3 != null ? Math.Round(item.subject3.Value, 2) : 0),
                        subject4 = (item.subject4 != null ? Math.Round(item.subject4.Value, 2) : 0),
                        subject5 = (item.subject5 != null ? Math.Round(item.subject5.Value, 2) : 0),
                        subject6 = (item.subject6 != null ? Math.Round(item.subject6.Value, 2) : 0),
                        subject7 = (item.subject7 != null ? Math.Round(item.subject7.Value, 2) : 0),
                        subject8 = (item.subject8 != null ? Math.Round(item.subject8.Value, 2) : 0),
                        subject9 = (item.subject9 != null ? Math.Round(item.subject9.Value, 2) : 0),
                        STT =item.STT,
                    });
                }
            }
            
            if (profile.id > 0)
            {
                profileStudent = await _context.ProfileStudents.FindAsync(profile.id);
                profileStudent.Updatedate = DateTime.Now;
                var InforMationProflies = _context.InforMationProflies.Where(x => x.idProfile == profile.id);
                        if (profile.Statust == 3)
                        {
                            profileStudent.CloseTime = DateTime.Now;
                            profile.Statust = 4;
                        }
             
                        inforMations = inforMations.OrderBy(X => X.STT).ToList();
                if (InforMationProflies != null)
                {
                    _context.InforMationProflies.RemoveRange(InforMationProflies);
                   await _context.SaveChangesAsync();
                   
                }
                    profile.InforMationProflies = inforMations.OrderBy(x => x.STT).ToList();
                }
            if (profile.id == 0)
            {
                profile.CreateDate = DateTime.Now;
                profile.Statust = 0;
                
            }
            if (profile.Statust == 1)
                {

                    profile.CreateDate = DateTime.Now;
                }
            profileStudent.idAccount = profile.idAccount;
            profileStudent.idAdmisstion = profile.idAdmisstion;
            profileStudent.Name = profile.Name;
            profileStudent.Nation = profile.Nation;
            profileStudent.Adress = profile.Adress;
            profileStudent.Areas = profile.Areas;
            profileStudent.Quantity = inforMations.Count;
            profileStudent.Religion = profile.Religion;
            profileStudent.CMND = profile.CMND;
            profileStudent.DateRange = profile.DateRange;
            profileStudent.BirthDay = profile.BirthDay;
            profileStudent.AdressRange = profile.AdressRange;
            profileStudent.Email = profile.Email;
            profileStudent.FromBirthDay = profile.FromBirthDay;
            profileStudent.Teletephone = profile.Teletephone;
            profileStudent.FromTelePhone = profile.FromTelePhone;
            profileStudent.imgavata = "";
            profileStudent.Sex = profile.Sex;
            profileStudent.idAccount = profile.idAccount;
            profileStudent.Shoo1 = profile.Shoo1;
            profileStudent.Shoo2 = profile.Shoo2;
            profileStudent.Shoo3 = profile.Shoo3;
            profileStudent.Year = profile.Year;
            profileStudent.CreateDate = profile.CreateDate;
            profileStudent.Priority_object = profile.Priority_object;
            profileStudent.Statust = profile.Statust;
            profileStudent.Mark = profile?.Mark;
            profileStudent.Identification = profile?.Identification;
           profileStudent.Type = admisstion.Type;
            profileStudent.IdBlock = profile.IdBlock;
            profileStudent.url= "/Admisstion/" + profile.idAdmisstion + "/" + profile.CMND.Trim() + "/" + profile.CMND.Trim()+"_" +profile.Name.Trim() + "_" + profile.idAdmisstion + ".pdf";  
            if ( profileStudent.id < 1)
            {
               await _context.ProfileStudents.AddAsync(profileStudent);
            }
            await _context.SaveChangesAsync();
                if(files!=null && files.Count > 0)
                {
                    var fileProfiles = await _context.FileProfiles.Where(x => x.idProfile == profileStudent.id).ToListAsync();
                    if (fileProfiles != null && fileProfiles.Count > 0)
                    {
                        _context.FileProfiles.RemoveRange(fileProfiles);
                        await _context.SaveChangesAsync();
                    }
                    var path1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admisstion/"+admisstion.id,profile.CMND.Trim());
                    if (Directory.Exists(path1))
                    {
                        string[] files1 = Directory.GetFiles(path1);
                        foreach (string file in files1)
                        {
                            File.SetAttributes(file, FileAttributes.Normal);
                            File.Delete(file);
                        }
                        Directory.Delete(path1);
                    }
                    Directory.CreateDirectory(path1);
                    foreach (var item in files)
                    {

                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/" + item.Url.ToString());
                        FileInfo fi = new FileInfo(path);
                        path1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admisstion/" + admisstion.id.ToString() + "/" + profile.CMND.Trim(), fi.Name);
                       
                        if (!Directory.Exists(path1))
                        {
                            File.Copy(path, path1, true);
                        }
                        else
                        {
                            File.SetAttributes(path1, FileAttributes.Normal);
                            File.Delete(path1);
                        }       
                        FileProfile fileProfile = new FileProfile();
                        fileProfile.idProfile = profileStudent.id;
                        fileProfile.url = "Admisstion/" + admisstion.id.ToString() + "/" + profile.CMND.Trim() + "/" + fi.Name;
                        fileProfile.Name = item.Name;
                        
                        await _context.FileProfiles.AddAsync(fileProfile);
                        await _context.SaveChangesAsync();
                    }
                }
                profileStudent.url=(admisstion?.Type == 1 ? _storageService.CreateTypeTwoPdf(profile) : _storageService.CreatePdf(profile));
                if (inforMations.Count > 0)
            {
                for (int i = 0; i < inforMations.Count; i++)
                {
                    inforMations[i].STT = (i + 1);
                }
                foreach (var item in inforMations)
                {
                        item.idProfile = profileStudent.id;
                    try {
                        Admisstion_Major major =  await _context.Admisstion_Major.SingleOrDefaultAsync(x => x.Major.delete != true && x.idMajor.Equals(item.idMajor) && x.idAdmisstion == profileStudent.idAdmisstion);
                     major.Count=(major.Count!=null?major.Count++:1);
                    } catch {  }
                }
                await _context.InforMationProflies.AddRangeAsync(inforMations);
              await  _context.SaveChangesAsync();
            }
            return new ApiResult() { Success = true, Message = "Thành Công", Data = profileStudent };
            }
            catch (Exception e) { return new ApiResult() { Success = false, Message = "Thật bại" + e.Message, Data = profileStudent }; }
          
        }

        public async Task<ApiResult> GetAdmisstionInfo(long? id)
        {
            try
            {
                if (id == null)
                {
                    return new ApiResult() { Success = false, Message = "id cannot value ", Data = null };
                }
             var Admisstion_Major =  _context.Admisstion_Major.Where(x=>x.idAdmisstion==id).ToList();
                if (Admisstion_Major == null || Admisstion_Major.ToList().Count==0)
                {
                    return new ApiResult() { Success = false, Message = "id Not Found", Data = null };
                }
                var block = _context.Addmisstion_Major_Block.ToList();
                var Majors = _context.Majors.Where(x=>x.delete!=true).ToList();
                var Admisstioninfo = (from c in Admisstion_Major
                                      join b in block on c.id equals b.idAdmisstion
                                      
                                      join m in Majors on c.idMajor equals m.id
                                      group c by new { c.id, c.idMajor, m.Name } into g
                                    
                                      select new
                                      {
                                          id= g.Key.id,
                                          idMajor =g.Key.idMajor,
                                          Name = g.Key.Name,
                                          Block= block.Where(x=>x.idAdmisstion== g.Key.id).Select(x=>x.idBlock)
                                      }).ToList();

                return new ApiResult() { Success = true, Message = "Thành Công ", Data = Admisstioninfo };
            }
            catch (Exception E)
            {

                return new ApiResult() { Success = false, Message = "Lỗi server " + E.Message, Data = null };
            }
        }

        public async Task<ApiResult> CreateProfile(long? id)
        {
            if (id == null)
            {
                return new ApiResult() { Success = false, Message = "ID không đc bỏ trống ", Data = null };
            }
    
            
            var School = _context.Schools.Select(x => x).ToList();
            var Shool1 = (from c in School
                          select new SChool
                          {
                              id = c.id,
                              NameSchool = c.NameShool,
                              idSchool = c.idShool.Trim() + "/" + c.idDistrict.Trim() + "/" + c.idConscious.Trim(),
                              idConscious = c.idConscious.Trim(),
                              idDistrict = c.idDistrict.Trim() + "/" + c.idConscious.Trim()
                          });
            var Conscious = (from c in School
                             group School by new { c.idConscious, c.NameConscious } into g
                             select new Conscious
                             {
                                 NameConscious = g.Key.NameConscious,
                                 idConscious = g.Key.idConscious.Trim()

                             });
            var District = (from c in School
                            group School by new { c.idDistrict, c.NameDistrict, c.idConscious } into g
                            select new District
                            {
                                NameDistrict = g.Key.NameDistrict,
                                idDistrict = g.Key.idDistrict.Trim() + "/" + g.Key.idConscious.Trim(),
                                idConscious = g.Key.idConscious.Trim()
                            });
            //var block = _context.Addmisstion_Major_Block.Select(X => new { X.id, X.idBlock,X.idAdmisstion}).ToList();
            var Majors = _context.Majors.Where(x => x.delete != true).Select(X => new { X.id,X.Name}).ToList();
            var Admisstion_Major = await _context.Admisstion_Major.Where(x => x.idAdmisstion == id).ToListAsync();
            //var Major = (from c in Admisstion_Major
            //                     // join b in block on c.id equals b.idAdmisstion
            //                      join m in Majors on c.idMajor equals m.id
            //                    //  group m by new {  b.idBlock } into g
            //                      select new
            //                      {
            //                          id = g.Key.idBlock,
            //                          Majors = g
            //                      }).ToList();

            var MajorsData = (from c in Admisstion_Major

                              join m in Majors on c.idMajor equals m.id

                              select new
                              {
                                  id = m.id,
                                  Name = m.Name,
                              }).ToList();
            ProfileStudent student = new ProfileStudent();
            ProfileView view = new ProfileView();
            view.Conscious = Conscious.ToList();
            view.District = District.ToList();
            view.SChool = Shool1.ToList();
            view.Data = student;
            view.Majors = MajorsData;
            view.CountMajo = Admisstion_Major.Count;
            return new ApiResult() { Success = true, Message = "Không tìm thấy", Data = view };
        }

        public  async Task<ApiResult> GetProfiles(long? id)
        {
            try {
                var Profile = await _context.ProfileStudents.Where(x => x.idAdmisstion == id && x.Statust == 2).ToListAsync();
                return new ApiResult() { Success = true, Message = "Không tìm thấy", Data = Profile };
            } catch (Exception e) {
                return new ApiResult() { Success = false, Message = "Lỗi server "+e.Message, Data = null };
            }
            
        }

        public async Task<ApiResult> ExportExcell(long? id)
        {
            try
            {
                var Profile = await _context.ProfileStudents.Where(x => x.idAdmisstion == id && x.Statust == 1).ToListAsync();
               // var ProfileInfor = await _context.InforMationProflies.ToListAsync();
             //   var Major = await _context.Majors.ToListAsync();
               // var Block = await _context.Blocks.ToListAsync();
                //var result = (from p in Profile
                //              join I in ProfileInfor on p.id equals I.idProfile
                           
                //              select new
                //              {
                //                  SBD = "GSA" + I.id,
                //                  CMND = p.CMND,
                //                  HOTEN = p.Name,
                //                  NGAYSINH = p.BirthDay.Value.ToShortDateString(),
                //                  KHUVUA = p.Areas,
                //                  DOITUONG = (p.Priority_object != null ? p.Priority_object : ""),
                //                  MANGANH = I.idMajor,
                //                  TENNGANH = Major.SingleOrDefault(x=>x.id==I.idMajor).Name,
                //                  MATOHOP=I.idBlock,
                //                  TENTOHOP= Block.SingleOrDefault(x => x.id == I.idBlock).Desscription,
                //                  L10_Mon1=I.subject1,
                //                  L10_Mon2 = I.subject2,
                //                  L10_Mon3 = I.subject3,
                              
                //                  L11_Mon1 = I.subject4,
                //                  L11_Mon2 = I.subject5,
                //                  L11_Mon3 = I.subject6,
                //                  L12_Mon1 = I.subject7,
                //                  L12_Mon2 = I.subject8,
                //                  L12_Mon3 = I.subject9,
                //                  SUM = ((double)((I.subject1+I.subject4+I.subject7)/3)+ ((I.subject2 + I.subject5 + I.subject8) / 3)+ ((I.subject3 + I.subject6 + I.subject9) / 3)),
                //                  STTNV=I.STT


                //              });



                return new ApiResult() { Success = true, Message = "Không tìm thấy", Data = Profile };
             
            }
            catch (Exception e)
            {
                return new ApiResult() { Success = false, Message = "Lỗi server " + e.Message, Data = null };
            }
        }

        public async Task<ApiResult> CheckProFile(long id ,string uid)
        {
            if (id == 0)
            {
                return new ApiResult() { Success = false, Message = "ID không đc bỏ trống ", Data = null };
            }
            var Admisstion= await _context.Admisstions.Where(x => x.id == id)?.FirstOrDefaultAsync();


            if (Admisstion==null)
            {
                return new ApiResult() { Success = false, Message = "Không tìm thấy", Data = null };
            }

            var Profile = await _context.ProfileStudents.Where(x=> x.idAccount.Trim().Equals(uid.Trim()) && x.idAdmisstion==id).OrderByDescending(x=>x.id).ToListAsync();
            if(Profile.Count > 0)
            {
                return new ApiResult() { Success = false, Message = "Tìm tìm thấy", Data = Profile.FirstOrDefault()};
            }

            return new ApiResult() { Success = true, Message = "Không tìm thấy", Data =null };

        }

        public async Task<ApiResult> GetAllProFileStudent(string uid)
        {
            if (uid == null)
            {
                return new ApiResult() { Success = false, Message = "ID không đc bỏ trống ", Data = null };
            }
          await  CheckProFile();
            var profile = _context.ProfileStudents.Where(x => x.idAccount.Trim().Equals(uid.Trim()) && x.Statust!=0).ToList();
           

            var listinfor = (from s in profile
                             orderby s.CreateDate descending
                             select new ProfileStudentsView
                             {
                                 id = s.id,
                                 idAccount = s.idAccount,
                                 idAdmisstion = s.idAdmisstion,
                                 CreateDate = s.CreateDate,
                                 Name = s.Name,
                                 Email = s.Email,
                                 Teletephone = s.Teletephone,
                                 url = s.url,
                                 CMND = s.CMND,
                                 Statust =s.Statust,
                                 Quantity = s.Quantity,
                                 CloseTime = (s.CloseTime)

                             }).ToList();

            return new ApiResult() { Success = true, Message = "Danh Sách Hồ Sơ", Data = listinfor };
        }

        public async Task<ApiResult> DeleteBlock(string id)
        {
            var Block = await _context.Blocks.FindAsync(id);
            if (Block == null)
            {
                return new ApiResult() { Message = "Không tìm thấy",Data=null,Success=false};
            }
            Block.Delete = true;
          await  _context.SaveChangesAsync();
            return new ApiResult() { Message = "Xóa thành công", Data = null, Success = true };
        }

        public async  Task<ApiResult> DowloadFile(long? id, int type)
        {
            string inputDir = "";
            Admisstion admisstion = await _context.Admisstions.FindAsync(id);
            if (type == 1)
            {
                if (admisstion == null)
                {
                    return new ApiResult() { Success = false, Message = "Không tìm thấy", Data = null };
                }
                inputDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Admisstion",admisstion.id.ToString());
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\dowload\\data.rar");
                if (File.Exists(path))
                {
                    await Task.Run(() => File.Delete(path));
                }

                // File đầu ra sau khi nén thư mục trên.
                string zipPath = path;
                ZipFile.CreateFromDirectory(inputDir, zipPath, CompressionLevel.Fastest, true);
                return new ApiResult() { Success = true, Message = ""+admisstion.id+ ".rar", Data = "/dowload/data.rar" };
            }
            else
            {
                var Profile = await _context.ProfileStudents.FindAsync(id);
                if (Profile == null)
                {
                    return new ApiResult() { Success = false, Message = "Không tìm thấy", Data = null };
                }
                inputDir = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Admisstion/{Profile.idAdmisstion}/{Profile.CMND.Trim()}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\dowload\\data.rar");
                if (File.Exists(path))
                {
                    await Task.Run(() => File.Delete(path));
                }

                // File đầu ra sau khi nén thư mục trên.
                string zipPath = path;
                ZipFile.CreateFromDirectory(inputDir, zipPath, CompressionLevel.Fastest, true);
                return new ApiResult() { Success = true, Message = "" + Profile.CMND.Trim()+".rar", Data = "/dowload/data.rar" };
            }

          
        }
    }
}
