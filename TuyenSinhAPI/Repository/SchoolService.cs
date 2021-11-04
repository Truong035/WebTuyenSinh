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
    public class SchoolService : ISchoolService
    {
        private HeThongTuyenSinhDB _context;

        public SchoolService(HeThongTuyenSinhDB context)
        {
            _context = context;
        }

        public Task<ApiResult> Create(ExcellView request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> ListCreate(List<ExcellView> request)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> Update(string id, ExcellView request)
        {
            throw new NotImplementedException();
        }

        //public async Task<ApiResult> Create(ExcellView request)
        //{
        //    try {
        //        School school = new School();
        //        school.Adrees = request.Adrees;
        //        school.Area = request.Area;
        //        school.Name = request.NameShool;
        //        school.idDistrict = request.idDistrict;
        //        school.idShool = request.idShool;
        //      await  _context.Schools.AddAsync(school);
        //        await _context.SaveChangesAsync();
        //        return new ApiResult() { Success = true, Message = "Thành Công", Data = school };
        //    } catch (Exception e) {
        //        return new ApiResult() { Success = false, Message = "Lỗi Server "+e.Message, Data = null };
        //    }
        //}

        //public async Task<ApiResult> Delete(string id)
        //{
        //    try
        //    {

        //            if (id == null) { return new ApiResult() { Success = false, Message = "id Không được bỏ trống", Data = null }; }
        //        var shool = _context.Schools.FindAsync(id);
        //        if (await shool == null)
        //        {
        //            return new ApiResult() { Success = false, Message = "Không Tìm Thấy", Data = null };
        //        }
        //        _context.Schools.Remove(await shool);
        //       await _context.SaveChangesAsync();
        //            return new ApiResult() { Success = true, Message = "Thành Công", Data = shool };
        //    }
        //    catch (Exception e)
        //    {
        //        return new ApiResult() { Success = false, Message = "Lỗi Server " + e.Message, Data = null };
        //    }
        //}

        //public async Task<ApiResult> GetAll()
        //{
        //    try
        //    {
        //        var schools =await _context.Schools.Where(x => x.idShool!=null).Include(x => x.District).ThenInclude(x => x.Consciou).ToListAsync();
        //        return new ApiResult() { Success = true, Message = "Thành Công", Data = schools };
        //    }
        //    catch (Exception e)
        //    {
        //        return new ApiResult() { Success = false, Message = "Lỗi Server " + e.Message, Data = null };
        //    }
        //}

        //public async Task<ApiResult> GetByID(string id)
        //{
        //    try
        //    {
        //        if (id == null) { return new ApiResult() { Success = false, Message = "id Không được bỏ trống", Data = null }; }
        //        var shool = _context.Schools.Where(x=>x.idShool.Trim().Equals(id.Trim())).Include(x=>x.District).ThenInclude(x=>x.Consciou).ToList().First();
        //        if ( shool == null)
        //        {
        //            return new ApiResult() { Success = false, Message = "Không Tìm Thấy", Data = null };
        //        }
        //        return new ApiResult() { Success = true, Message = "Thành Công", Data = null };
        //    }
        //    catch (Exception e)
        //    {
        //        return new ApiResult() { Success = false, Message = "Lỗi Server " + e.Message, Data = null };
        //    }
        //}

        //public async Task<ApiResult> ListCreate( List<ExcellView> request)
        //{

        //        foreach (var item in request)
        //        {
        //        try
        //        {

        //            if (item.idConscious != null && item.idDistrict != null && item.idShool != null)
        //            {
        //                var listConscious = await _context.Conscious.FindAsync(item.idConscious);
        //                if (listConscious == null)
        //                {
        //                    listConscious = new Consciou() { 
        //                    idConscious=item.idConscious,
        //                    Name=item.NameConscious,
        //                    Delete=false, 
        //                    };
        //                    await _context.Conscious.AddAsync(listConscious);
        //                    await _context.SaveChangesAsync();
        //                }
        //                else if(listConscious.Delete == true){
        //                    listConscious.Delete = false;
        //                   await _context.SaveChangesAsync();
        //                }

        //          var   district = await _context.Districts.FindAsync(item.idDistrict);
        //                if (district == null)
        //                {
        //                    district = new District()
        //                    {
        //                        idDistrict = item.idDistrict,
        //                        Name = item.NameDistrict,
        //                        idConscious=item.idConscious,
        //                        Delete = false,
        //                    };
        //                  await  _context.Districts.AddAsync(district);
        //                    await _context.SaveChangesAsync();
        //                }
        //                else if (district.Delete == true)
        //                {
        //                    district.Delete = false;
        //                    await _context.SaveChangesAsync();

        //                }
        //                School school= await _context.Schools.FindAsync(item.idShool);
        //                if (school == null)
        //                {
        //                    school = new School();
        //                    school.Adrees = (item.Adrees != null ? item.Adrees : "");
        //                    school.Area = (item.Area != null ? item.Area : "");
        //                    school.Name = item.NameShool;
        //                    school.idDistrict = district.idDistrict;
        //                    school.idShool = item.idShool;

        //                    await _context.Schools.AddAsync(school);
        //                    await _context.SaveChangesAsync();
        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {

        //        }
        //    }
        //    return new ApiResult() { Success = true, Message = "Thành Công", Data = request };

        //}

        //public async Task<ApiResult> Update(string id, ExcellView request)
        //{
        //    try
        //    {


        //        return new ApiResult() { Success = true, Message = "Thành Công", Data = null };
        //    }
        //    catch (Exception e)
        //    {
        //        return new ApiResult() { Success = false, Message = "Lỗi Server " + e.Message, Data = null };
        //    };
        //}
    }
}
