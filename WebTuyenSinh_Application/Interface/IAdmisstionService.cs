using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh_Application.Modell;
using WebTuyenSinh_Application.ViewApi;
using WebTuyenSinh.Data.Entityes;

namespace WebTuyenSinh_Application.Interface
{
    public interface IAdmisstionService
    {
        public Task<ApiResult> CreateAdmisstion(AdmisstionCreate request);
        public Task<ApiResult> UpdateAdmisstion(long? id, AdmisstionCreate request);
        public Task<ApiResult> DeleteAdmisstion(long? id);
        public Task<ApiResult> GetAll();
        public Task<ApiResult> CreateAdmisstion_Major(List<Admisstion_MajorCreate> request, long? id);
        public Task<ApiResult> GetByID(long? id);
        public Task<ApiResult> DeleteAdmisstion_Major(long? id);
        public Task<ApiResult> GetBockAll();
        public Task<ApiResult> UpdateStatusAdmisstion_Major(long? id, Admisstion_MajorUpdateStatus updateStatus);
        public Task<ApiResult> CreateListBlock(List<Block> blocks);
        public Task<ApiResult> GetData();
    }
}
