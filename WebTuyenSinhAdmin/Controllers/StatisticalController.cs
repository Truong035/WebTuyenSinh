using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh_Application.Interface;

namespace WebTuyenSinhAdmin.Controllers
{
    public class StatisticalController : Controller
    {
        private readonly ILogger<StatisticalController> _logger;
        private IStatisticalService _statistical;
        public StatisticalController(ILogger<StatisticalController> logger, IStatisticalService statistical)
        {
            _logger = logger;
            _statistical = statistical;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Statistical(DateTime? fromDate, DateTime? toDate, long? idAdmisstion, string idMajor)
        {
            return Ok(await _statistical.Statistical(fromDate,toDate, idAdmisstion,idMajor));
        }
        
    }
}
