using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.Modell;
using WebTuyenSinh_Application.ViewApi;
using WebTuyenSinh.Data.Entityes;

namespace WebTuyenSinh_Application.Repository
{
    public class ExcellService : IExcellService
    {
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;
        private string USER_CONTENT_FOLDER_NAME = "Document";
        private readonly string _userContentFolder;
        private readonly HeThongTuyenSinhDB _context;
        public ExcellService(IWebHostEnvironment webHostEnvironment, IHostingEnvironment hostingEnvironment, HeThongTuyenSinhDB context)
        {
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        public Task DeleteFileAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public  async Task<ApiResult> SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            try
            {
                var filePath = Path.Combine(_userContentFolder, fileName);
                using var output = new FileStream(filePath, FileMode.Create);
                await mediaBinaryStream.CopyToAsync(output);
                return new ApiResult() { Message = "Ok", Success = true, Data = "/Images/Lecture/" + fileName };
            }
            catch (Exception e)
            {
                return new ApiResult() { Message = "Error " + e.Message, Success = false, Data = e.Message };
            }


        }

        
    }
}
