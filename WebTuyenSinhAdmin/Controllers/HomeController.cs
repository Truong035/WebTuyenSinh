using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinhAdmin.Models;

namespace WebTuyenSinhAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IStatisticalService _statistical;
        public HomeController(ILogger<HomeController> logger , IStatisticalService statistical)
        {
            _logger = logger;
            _statistical = statistical;
        }

        public async Task<IActionResult> Index(int? year)
        {
            if (year == null)
            {
                year = DateTime.Now.Year;
            }
            string cookieValueFromReq = Request.Cookies["Token"];
            return View(await _statistical.StatisticalHome(year.Value));
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
