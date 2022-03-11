using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.Modell;
using WebTuyenSinh_Application.ViewApi;

namespace WebTuyenSinh_Application.Interface
{
  public interface ISchoolService
    {
        public Task<ApiResult> Create(School request);
        public Task<ApiResult> ListCreate(List<School> request);
        public Task<ApiResult> Update(string id, School request);
        public Task<ApiResult> Delete(long id);
        public Task<List<School>> GetAll();
        public Task<ApiResult> GetByID(long ? id);

    }
}
