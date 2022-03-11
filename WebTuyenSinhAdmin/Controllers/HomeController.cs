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
using WebTuyenSinhAdmin.Contan;
using WebTuyenSinhAdmin.Models;

namespace WebTuyenSinhAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IStatisticalService _statistical;
        private static readonly string[] Scopes = new[] { DriveService.Scope.DriveFile, DriveService.Scope.Drive };
        private static Google.Apis.Drive.v3.DriveService driveService;
        public HomeController(ILogger<HomeController> logger , IStatisticalService statistical)
        {
            _logger = logger;
            _statistical = statistical;

        }
        private UserCredential GetCredentials()
        {
            UserCredential credential;

            using (var stream = new FileStream("code_secret_client.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

                credPath = Path.Combine(credPath, "code_secret_client.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
              
            }

            return credential;
        }
        public async Task<IActionResult> Index(int? id)
        {

            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }

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
