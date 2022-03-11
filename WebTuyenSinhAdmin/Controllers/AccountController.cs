using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.Modell;
using WebTuyenSinh_Application.ViewApi;
using WebTuyenSinhAdmin.Contan;

namespace WebTuyenSinhAdmin.Controllers
{
    public class AccountController : Controller
    {
        private ILoginService _login;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountController(ILoginService login, IHttpContextAccessor httpContextAccessor)
        {
            _login = login;
            this._httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
         //   // Thư mục sẽ được nén.
         ////   var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Profile");
         //   string inputDir =Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Profile");
         //   var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data.zip");
         //   // File đầu ra sau khi nén thư mục trên.
         //   string zipPath = path;

         //   // Giải nén file zip ra một thư mục.
         //   string extractPath = "D:/outputdir";

         //   // Tạo ra file zip bằng cách nén một thư mục.
         //   ZipFile.CreateFromDirectory(inputDir, zipPath, CompressionLevel.Fastest, true);

         //   // Giải nén file zip ra một thư mục.
         //   ZipFile.ExtractToDirectory(zipPath, extractPath);
            Response.Cookies.Delete("Token");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
        {
            if (ModelState.IsValid)
            {
         
                ApiResult apiResult = await _login.LoginUse(request);
                if (apiResult.Success)
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Append(UserContant.UseToken, apiResult.Data.ToString(), option);
                    return RedirectToAction("Index", "Home");

                }
                ModelState.AddModelError(nameof(request.Email), apiResult.Message);
            }
            return View(request);
        }
    }
}
