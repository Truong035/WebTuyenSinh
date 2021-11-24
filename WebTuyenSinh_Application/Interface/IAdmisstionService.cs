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
        public Task<ApiResult> GetAll(int? status);
        public Task<ApiResult> GetResult();
        public Task<ApiResult> CreateAdmisstion_Major(List<Admisstion_MajorCreate> request, long? id);
        public Task<ApiResult> GetByID(long? id);
        public Task<ApiResult> DeleteAdmisstion_Major(long? id);
        public Task<ApiResult> GetBockAll();
        public Task<ApiResult> UpdateStatusAdmisstion_Major(long? id, Admisstion_MajorUpdateStatus updateStatus);
        Task<ApiResult> GetInfoProfile(long? id);
        public Task<ApiResult> CreateListBlock(List<Block> blocks);
        Task<ApiResult> ListAll(long? id);
        public Task<ApiResult> GetData();
        public Task<ApiResult> DetailAdmisstion(long? id);
        public Task<ApiResult> ListProfile(long? id);
        public Task<ApiResult> UpdateStatusAdmisstion(long? id, AdmisstionCreate admisstionCreate);
        public Task<ApiResult> UpdateStatusProfile(long? id, string comment, DateTime? date, int? status);
        public Task<ApiResult> GetByProfile(long? id);
        public Task<ApiResult> CreateProfile(ProfileStudent profile);
        
        public Task<ApiResult> CreateProfile(long? id);
        public Task<ApiResult> GetAdmisstionInfo(long? id);
        public Task<ApiResult> GetProfiles(long? id);
        public Task<ApiResult> ExportExcell(long? id);
        public Task<ApiResult> CheckProFile(long id, string uid);
        public Task<ApiResult> GetAllProFileStudent(string uid);
    }
}