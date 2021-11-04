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
    public class SchoolService : ISchoolService
    {
        private HeThongTuyenSinhDB _context;

        public SchoolService(HeThongTuyenSinhDB context)
        {
            _context = context;
        }

        public async Task<ApiResult> Create(School request)
        {
            try {
                School school = new School();
                school.Adrees = request.Adrees;
                school.Area = request.Area;
                school.NameShool = request.NameShool;
                school.idDistrict = request.idDistrict;
                school.idShool = request.idShool;

                school.NameConscious = request.NameConscious;
                school.idConscious = request.idConscious;
                school.NameDistrict = request.NameDistrict;

                await  _context.Schools.AddAsync(school);
                await _context.SaveChangesAsync();
                return new ApiResult() { Success = true, Message = "Thành Công", Data = school };
            } catch (Exception e) {
                return new ApiResult() { Success = false, Message = "Lỗi Server "+e.Message, Data = null };
            }
        }

        public async Task<ApiResult> Delete(string id)
        {
            try
            {
                
                    if (id == null) { return new ApiResult() { Success = false, Message = "id Không được bỏ trống", Data = null }; }
                var shool = _context.Schools.FindAsync(id);
                if (await shool == null)
                {
                    return new ApiResult() { Success = false, Message = "Không Tìm Thấy", Data = null };
                }
                _context.Schools.Remove(await shool);
               await _context.SaveChangesAsync();
                    return new ApiResult() { Success = true, Message = "Thành Công", Data = shool };
            }
            catch (Exception e)
            {
                return new ApiResult() { Success = false, Message = "Lỗi Server " + e.Message, Data = null };
            }
        }

        public async Task<List<School>> GetAll()
        {
            try
            {
                var schools =await _context.Schools.Select(x=>x).ToListAsync();
                return schools;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<ApiResult> GetByID(long? id)
        {
            try
            {
                if (id == null) { return new ApiResult() { Success = false, Message = "id Không được bỏ trống", Data = null }; }
                var shool = await _context.Schools.FindAsync(id);
                if ( shool == null)
                {
                    return new ApiResult() { Success = false, Message = "Không Tìm Thấy", Data = null };
                }
                return new ApiResult() { Success = true, Message = "Thành Công", Data = null };
            }
            catch (Exception e)
            {
                return new ApiResult() { Success = false, Message = "Lỗi Server " + e.Message, Data = null };
            }
        }

        public async Task<ApiResult> ListCreate( List<School> request)
        {
            int c = 0;
            List<School> request1 = new List<School>();
                foreach (var item in request)
                {
             
              
                    if (item.idConscious != null && item.idDistrict != null && item.idShool != null)
                    {
                    var school = await _context.Schools.SingleOrDefaultAsync(x=>x.idShool.Equals(item.idShool) && x.idConscious.Equals(item.idConscious) &&
                    x.idDistrict.Equals(item.idDistrict)
                    );
                        if (school != null )
                        {
                        if (school.Adrees != item.Adrees || school.NameConscious!=item.NameConscious || school.NameDistrict!=item.NameDistrict || school.NameShool!= item.NameShool|| item.Area!= item.Area)
                        {
                            school.Adrees = item.Adrees;
                            school.Area = item.Area;
                            school.NameShool = item.NameShool;
                            school.idDistrict = item.idDistrict;
                            school.idShool = item.idShool;
                            school.NameConscious = item.NameConscious;
                            school.idConscious = item.idConscious;
                            school.NameDistrict = item.NameDistrict;
                            await _context.SaveChangesAsync();
                        }
                       
                     
                        //   await  Create(item);

                    }
                    else
                    {
                        request1.Add(item);
                    }

                
                    }
                c++;
                }
                if(request1.Count>0)
            {
                await _context.Schools.AddRangeAsync(request1);
                await _context.SaveChangesAsync();
            }
        

            return new ApiResult() { Success = true, Message = "Thành Công", Data = request1 };
           
        }

        public async Task<ApiResult> Update(string id, School request)
        {
            try
            {
              

                return new ApiResult() { Success = true, Message = "Thành Công", Data = null };
            }
            catch (Exception e)
            {
                return new ApiResult() { Success = false, Message = "Lỗi Server " + e.Message, Data = null };
            };
        }
    }
}
