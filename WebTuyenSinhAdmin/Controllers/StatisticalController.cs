using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinhAdmin.Contan;

namespace WebTuyenSinhAdmin.Controllers
{
    public class StatisticalController : Controller
    {
        private readonly IValidateTokenService _IValidateTokenService;
        private readonly ILogger<StatisticalController> _logger;
        private IStatisticalService _statistical;
        public StatisticalController(ILogger<StatisticalController> logger, IStatisticalService statistical, IValidateTokenService IValidateTokenService)
        {
            _logger = logger;
            _statistical = statistical;
            _IValidateTokenService = IValidateTokenService;
        }
        public async Task<IActionResult> Index()
        {
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
            var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.X_TK });
            if (check)
            {
                return View();
            }
            return View("UseNotFound");
        }

 

        public async Task<IActionResult> Statistical(DateTime? fromDate, DateTime? toDate, int? Type, string idMajor)
        {
            return Ok(await _statistical.Statistical(fromDate,toDate, Type, idMajor));
        }
        
    }
}
