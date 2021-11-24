using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuyenSinhAPI.Interface;
using TuyenSinhAPI.Modell;
using TuyenSinhAPI.ViewApi;
using WebTuyenSinh.Data.Entityes;

namespace TuyenSinhAPI.Repository
{
    public class ExcellService : IExcellService
    {
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;
        private string USER_CONTENT_FOLDER_NAME = "Document";
        private readonly string _userContentFolder;
        private readonly HeThongTuyenSinhDB _context;

        [Obsolete]
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

        public async Task<ApiResult> ImportAsync(Stream mediaBinaryStream, string fileName)
        {
            //var filePath = Path.Combine(_userContentFolder, fileName);
            //using var output = new FileStream(filePath, FileMode.Create);
            //await mediaBinaryStream.CopyToAsync(output);

            List<ExcellView> excellViews = new List<ExcellView>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            int cout  = 0;
            using (var render = ExcelDataReader.ExcelReaderFactory.CreateReader(mediaBinaryStream))
            {
                while ( render.Read()&& cout<1000)
                {  
                    try
                    {
                        cout++;
                        string address = "";
                        try
                        {
                            address += render.GetValue(7).ToString();
                        }
                        catch { }
                        excellViews.Add(new ExcellView()
                        {
                            idConscious = render.GetValue(1).ToString(),
                            NameConscious = render.GetValue(2).ToString(),
                            idDistrict = render.GetValue(3).ToString(),
                            NameDistrict = render.GetValue(4).ToString(),
                            idShool = render.GetValue(5).ToString(),
                            NameShool = render.GetValue(6).ToString(),
                            Area = render.GetValue(8).ToString(),
                            Adrees = address,
                        }); ;; ;
                     
                    }
                    catch { }
                }
            }
            try { excellViews.RemoveAt(0); } catch { }
            return new ApiResult() { Success = false, Message = "Success", Data = excellViews };

        }
    }
}
