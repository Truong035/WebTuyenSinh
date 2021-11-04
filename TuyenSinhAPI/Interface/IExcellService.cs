using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TuyenSinhAPI.ViewApi;

namespace TuyenSinhAPI.Interface
{
    public interface IExcellService
    {
        public Task<ApiResult> SaveFileAsync(Stream mediaBinaryStream, string fileName);
        public Task<ApiResult> ImportAsync(Stream mediaBinaryStream, string fileName);
        public Task DeleteFileAsync(string fileName);
    }
}
