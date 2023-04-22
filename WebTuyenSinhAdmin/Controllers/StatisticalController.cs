using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.ViewApi;
using WebTuyenSinhAdmin.Contan;
using WebTuyenSinh.Data.Entityes;
namespace WebTuyenSinhAdmin.Controllers
{
    public class StatisticalController : Controller
    {
        private readonly IAdmisstionService _service;
        private readonly IValidateTokenService _IValidateTokenService;
        private readonly ILogger<StatisticalController> _logger;
        private IStatisticalService _statistical;
        public StatisticalController(ILogger<StatisticalController> logger, IStatisticalService statistical, IValidateTokenService IValidateTokenService, IAdmisstionService service)
        {
            _logger = logger;
            _statistical = statistical;
            _IValidateTokenService = IValidateTokenService;
            _service = service;
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
                ApiResult result = await _service.GetAll(4);
                List<Admisstion> Admisstion = (List<Admisstion>)result.Data;
                return View(Admisstion);
            }
            return View("UseNotFound");
        }

 

        public async Task<IActionResult> Statistical(DateTime? fromDate, DateTime? toDate, int? Type, string idMajor,long? idAdmisstion,bool? Statust)
        {
            return Ok(await _statistical.Statistical(fromDate,toDate, Type, idMajor, idAdmisstion,  Statust));
        }
        
    }
}
