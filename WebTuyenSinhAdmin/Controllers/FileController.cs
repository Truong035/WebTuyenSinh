using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh_Application.Modell;

namespace WebTuyenSinhAdmin.Controllers
{
    public class FileController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public FileController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddReplyDetails()
        {
            var files = Request.Form.Files;
            List<FileProfileView> url = new List<FileProfileView>();
                foreach (IFormFile photo in files)
                {
                string name = GetUniqueFileName(photo.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Profile", name);
                    var stream = new FileStream(path, FileMode.Create);
                   await photo.CopyToAsync(stream);
                url.Add(new FileProfileView()
                {
                    Url = "Profile/" + name,
                    Name = photo.FileName
                }) ;
                }
            return Ok(url);
        }
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }

    }
}
