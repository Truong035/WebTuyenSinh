using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.ViewApi;

namespace WebTuyenSinh_Application.System
{
    public interface IStorageService
    {
        string GetFileUrl(string fileName);
        Task SaveFileAsync(Stream mediaBinaryStream, string fileName);
         string CreatePdf(ProfileStudent profile);
        Task DeleteFileAsync(string fileName);
        Task<ApiResult> ExportExcell(long? id);
        Task<ApiResult> ExportExcellOne(long? id);
        string CreateTypeTwoPdf(ProfileStudent profile);
    }
}
