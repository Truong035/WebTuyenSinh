using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuyenSinhAPI.Modell;
using TuyenSinhAPI.ViewApi;

namespace TuyenSinhAPI.Interface
{
  public interface ISchoolService
    {
        public Task<ApiResult> Create(ExcellView request);
        public Task<ApiResult> ListCreate(List<ExcellView> request);
        public Task<ApiResult> Update(string id, ExcellView request);
        public Task<ApiResult> Delete(string id);
        public Task<ApiResult> GetAll();
        public Task<ApiResult> GetByID(string id);

    }
}
