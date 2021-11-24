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
                admisstion.Description = request.Description;
                admisstion.Statust = 0;
                admisstion.Delete = false;
                admisstion.Type = request.Type;
                await _context.Admisstions.AddAsync(admisstion);
                var id = await _context.SaveChangesAsync();
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
                admisstion.Delete = false;
            //    admisstion.Statust = false;
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
                    }
        
                    else if (item.Statust == 3)
                    {
                        item.Statust = 2;
                    }
                    else if (item.Statust == 0)
                    {
                        item.Statust = 1;
                    }
                }
                await _context.SaveChangesAsync();
            } catch { }
            
        }
        private async Task CheckProFile()
        {
            List<Admisstion> admisstions = await _context.Admisstions.Where(x => x.Statust==2 && x.Delete != true).ToListAsync();
            foreach (var item in admisstions)
            {
                //var admisstion=await _context.Admisstion_Major.Where(x=>x.Statust)

            }
        }
            public async Task<ApiResult> GetAll(int? status)
        {
            try
            {
                await Checkupdate();
                List<Admisstion> admisstions = await _context.Admisstions.Where(x => x.Delete != true && x.Statust < status).OrderByDescending(x => x.id).ToListAsync();
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
                admisstion.Admisstion_Major = await _context.Admisstion_Major.Where(x => x.Delete != true && x.idAdmisstion==admisstion.id
                ).Include(x=>x.Major).Include(x => x.Addmisstion_Major_Block).ToListAsync();
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
                return new ApiResult() { Success = false, Message = "Thành Công" , Data = admisstion };
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
                //  Admisstion admisstion1 = await _context.Admisstions.FindAsync(admisstion.idAdmisstion);
                var profile = _context.ProfileStudents.Where(x => x.idAdmisstion == admisstion.idAdmisstion).ToList();
                var infor = _context.InforMationProflies.ToList();
                List<Addmisstion_Major_Block> Blocks = _context.Addmisstion_Major_Block.Where(x => x.idAdmisstion == admisstion.id).ToList();
                var listinfor = (from s in profile
                                 join i in infor on s.id equals i.idProfile
                                 join b in Blocks on i.idBlock equals b.idBlock
                                 where i.idMajor==admisstion.idMajor
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
                admisstion.CloseTime = admisstionCreate.CloseTime;
                admisstion.Description = admisstionCreate.Description;
                await _context.SaveChangesAsync();

                foreach (var item in admisstionCreate.Majors)
                {

                        if (item.OpenTime < admisstion.OpenTime)
                        {
                            item.OpenTime = admisstion.OpenTime;
                        }
                        if (item.CloseTime > admisstion.CloseTime)
                        {
                            item.CloseTime = admisstion.CloseTime;
                        }
                        var major = _context.Admisstion_Major.SingleOrDefault(x => x.idMajor.Trim().Equals(item.idMajor.Trim()) && x.idAdmisstion == id && x.Delete != true);

                        major.OpenTime = item.OpenTime;
                        major.CloseTime = item.CloseTime;
                        major.Statust = item.Statust;
                        await _context.SaveChangesAsync();
                }
            }
            catch { }
                return new ApiResult() { Success = true, Message = "Thành công", Data = null };
            }

        public async Task<ApiResult> UpdateStatusProfile(long? id, string comment, DateTime? date, int? status)
        {
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
                await _context.SaveChangesAsync();
                if (status == 2)
                {
                    new EmailHelper().SendEmail(ProfileStudents.Email, comment, "Thông Báo Từ Đại học giao thông vận tải");
                }
           
            }
            catch { }
            return new ApiResult() { Success = true, Message = "Thành công", Data = null };
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
                //  Admisstion admisstion1 = await _context.Admisstions.FindAsync(admisstion.idAdmisstion);
                var profile = _context.ProfileStudents.Where(x => x.idAdmisstion == id).ToList();
             
    
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
          
            ProfileView view = new ProfileView();
            view.Conscious = Conscious.ToList();
            view.District = District.ToList();
            view.SChool = Shool1.ToList();
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
            ProfileView view = new ProfileView();
            view.Conscious = Conscious.ToList();
            view.District = District.ToList();
            view.SChool = Shool1.ToList();
            view.Data = student;

            return new ApiResult() { Success = true, Message = "Không tìm thấy", Data = view };
        }

        public async Task<ApiResult> CreateProfile(ProfileStudent profile)
        {
            ProfileStudent profileStudent = new ProfileStudent();

            List<InforMationProflie> inforMations = new List<InforMationProflie>();
            foreach (var item in profile.InforMationProflies)
            {
                if (!inforMations.Exists(x => x.idMajor.Trim().Equals(item.idMajor.Trim()) && x.idBlock.Trim().Equals(item.idBlock.Trim())))
                { 
                    inforMations.Add(new InforMationProflie() { 
                    idBlock=item.idBlock,
                    idMajor=item.idMajor,
                        subject1 = item.subject1,
                        subject2 = item.subject2,
                        subject3 = item.subject3,
                        subject4 = item.subject4,
                        subject5 = item.subject5,
                        subject6 = item.subject6,
                        subject7 = item.subject7,
                        subject8 = item.subject8,
                        subject9 = item.subject9,
                        STT=item.STT,
                    });;
                }

            }
            if (profile.id > 0)
            {
                profileStudent = await _context.ProfileStudents.FindAsync(profile.id);
                profileStudent.Updatedate = DateTime.Now;
                var InforMationProflies = _context.InforMationProflies.Where(x => x.idProfile == profile.id);
                profile.Statust = 3;
                inforMations = inforMations.OrderBy(X => X.STT).ToList();
                if (InforMationProflies != null)
                {
                    _context.InforMationProflies.RemoveRange(InforMationProflies);
                   await _context.SaveChangesAsync();
                   
                }
            }
            if (profile.id == 0)
            {
                profile.CreateDate = DateTime.Now;
                profile.Statust = 0;
                var admisstion = _context.Admisstions.FindAsync(profile.idAdmisstion);
                
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
            profileStudent.imgavata = profile.imgavata;
            profileStudent.Sex = profile.Sex;
            profileStudent.idAccount = profile.idAccount;
            profileStudent.Type = profile.Type;
            profileStudent.Shoo1 = profile.Shoo1;
            profileStudent.Shoo2 = profile.Shoo2;
            profileStudent.Shoo3 = profile.Shoo3;
            profileStudent.Year = profile.Year;
            profileStudent.CreateDate = profile.CreateDate;
            profileStudent.Priority_object = profile.Priority_object;
            profileStudent.Statust = profile.Statust;
            profileStudent.url = await _storageService.CreatePdf(profile);
            if ( profileStudent.id < 1)
            {
               await _context.ProfileStudents.AddAsync(profileStudent);
            }
            await _context.SaveChangesAsync();

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
                    } catch { }
                }
                await _context.InforMationProflies.AddRangeAsync(inforMations);
              await  _context.SaveChangesAsync();
            }
            return new ApiResult() { Success = true, Message = "Thành Công", Data = profileStudent };
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
            ProfileStudent student = new ProfileStudent();
            ProfileView view = new ProfileView();
            view.Conscious = Conscious.ToList();
            view.District = District.ToList();
            view.SChool = Shool1.ToList();
            view.Data = student;
            return new ApiResult() { Success = true, Message = "Không tìm thấy", Data = view };
        }

        public  async Task<ApiResult> GetProfiles(long? id)
        {
            try {
                var Profile = await _context.ProfileStudents.Where(x => x.idAdmisstion == id && x.Statust == 1).ToListAsync();
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
                var ProfileInfor = await _context.InforMationProflies.ToListAsync();
                var School = await _context.Schools.ToListAsync();
                var Major = await _context.Majors.ToListAsync();
                var Block = await _context.Blocks.ToListAsync();

                var result = (from p in Profile
                              join I in ProfileInfor on p.id equals I.idProfile
                           
                              select new
                              {
                                  SBD = "GSA" + I.id,
                                  CMND = p.CMND,
                                  HOTEN = p.Name,
                                  NGAYSINH = p.BirthDay.Value.ToShortDateString(),
                                  KHUVUA = p.Areas,
                                  DOITUONG = (p.Priority_object != null ? p.Priority_object : ""),
                                  MANGANH = I.idMajor,
                                  TENNGANH = Major.SingleOrDefault(x=>x.id==I.idMajor).Name,
                                  MATOHOP=I.idBlock,
                                  TENTOHOP= Block.SingleOrDefault(x => x.id == I.idBlock).Desscription,
                                  L10_Mon1=I.subject1,
                                  L10_Mon2 = I.subject2,
                                  L10_Mon3 = I.subject3,
                              
                                  L11_Mon1 = I.subject4,
                                  L11_Mon2 = I.subject5,
                                  L11_Mon3 = I.subject6,
                                  L12_Mon1 = I.subject7,
                                  L12_Mon2 = I.subject8,
                                  L12_Mon3 = I.subject9,
                                  SUM = ((double)((I.subject1+I.subject4+I.subject7)/3)+ ((I.subject2 + I.subject5 + I.subject8) / 3)+ ((I.subject3 + I.subject6 + I.subject9) / 3)),
                                  STTNV=I.STT


                              });



                return new ApiResult() { Success = true, Message = "Không tìm thấy", Data = Profile };
             
            }
            catch (Exception e)
            {
                return new ApiResult() { Success = false, Message = "Lỗi server " + e.Message, Data = null };
            }
        }

        public async Task<ApiResult> CheckProFile(long id ,string uid)
        {
            if (id == null)
            {
                return new ApiResult() { Success = false, Message = "ID không đc bỏ trống ", Data = null };
            }
            var Profile = await _context.ProfileStudents.Where(x => x.idAdmisstion == id && x.idAccount.Trim().Equals(uid.Trim())).ToListAsync();
            if(Profile.Count > 0)
            {
                return new ApiResult() { Success = false, Message = "Không tìm thấy", Data = Profile };
            }

            return new ApiResult() { Success = true, Message = "Không tìm thấy", Data =null };

        }

        public async Task<ApiResult> GetAllProFileStudent(string uid)
        {
            if (uid == null)
            {
                return new ApiResult() { Success = false, Message = "ID không đc bỏ trống ", Data = null };
            }
            var profile = _context.ProfileStudents.Where(x => x.idAccount.Trim().Equals(uid.Trim())).ToList();


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
                                 Statust = s.Statust,
                                 Quantity = s.Quantity

                             }).ToList();

            return new ApiResult() { Success = true, Message = "Danh Sách Hồ Sơ", Data = listinfor };
        }
    }
}
