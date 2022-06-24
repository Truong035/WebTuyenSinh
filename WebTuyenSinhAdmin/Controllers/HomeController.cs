using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.Modell;
using WebTuyenSinhAdmin.Contan;
using WebTuyenSinhAdmin.Models;

namespace WebTuyenSinhAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IStatisticalService _statistical;
        private readonly IValidateTokenService _IValidateTokenService;
        public HomeController(ILogger<HomeController> logger , IStatisticalService statistical, IValidateTokenService IValidateTokenService)
        {
            _logger = logger;
            _statistical = statistical;
            _IValidateTokenService = IValidateTokenService;

        }
     
        public async Task<IActionResult> Index(int? id)
        {

            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
            ResetPassword resetPassword = await _IValidateTokenService.ValidateToken(cookie);
            ViewBag.UseName = resetPassword.UseName;
            //var credential = GetCredentials();
            //driveService = new Google.Apis.Drive.v3.DriveService(new BaseClientService.Initializer()
            //{
            //    HttpClientInitializer=credential,
            //    ApplicationName="Hahahaha"
            //});
            //Google.Apis.Drive.v3.FilesResource.ListRequest listRequest = driveService.Files.List();

            return View(await _statistical.StatisticalHome(id));
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
