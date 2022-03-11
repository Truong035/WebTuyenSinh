using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.Modell;
using WebTuyenSinh_Application.ViewApi;
using WebTuyenSinhAdmin.Contan;

namespace WebTuyenSinhAdmin.Controllers
{
    public class ManagerSchoolController : Controller
    {
        private readonly IValidateTokenService _IValidateTokenService;
        private readonly ISchoolService service;
        private readonly IExcellService _IExcellService;
        public ManagerSchoolController(ISchoolService service, IExcellService IExcellService,
            IValidateTokenService IValidateTokenService)
        {
            this.service = service;
            _IExcellService = IExcellService;
            _IValidateTokenService = IValidateTokenService;
        }

        public async Task<IActionResult> Index(string ShoolName, int? page=0)
        {
            string cookie = Request.Cookies[UserContant.UseToken];
    
            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
            
            List<School> schools =(List<School>) await  service.GetAll();
            if (ShoolName != null && ShoolName.Length > 0) { schools = schools.Where(x => x.NameShool.Contains(ShoolName)).ToList(); } else
            {
                ShoolName = "";
            }
                int limit = 10;
            int start;
            if (page > 0)
            {
                page = page;
            }
            else
            {
                page = 1;
            }
            start = (int)(page - 1) * limit;
            ViewBag.pageCurrent = page;
            ViewBag.Search = ShoolName;
            int totalProduct = schools.Count;
            float numberpage = totalProduct / limit;
            ViewBag.numberPage = (int) Math.Ceiling(numberpage);
           
            var check  = await  _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TH,PermisstionConTant.QL_TS});
            if (check)
            {
                return View(schools.Skip(start).Take(limit).ToList()); ;
            }
            return View("IndexUse", schools.Skip(start).Take(limit).ToList());
        }

        public IActionResult ViewExcel()
        {
            try {
                string cookie = Request.Cookies[UserContant.UseToken];

                if (cookie == null || cookie.Length == 0)
                {
                    return RedirectToAction("Index", "Account");
                }
                string shool = HttpContext.Session.GetString("School");
                var api = JsonConvert.DeserializeObject<List<School>>(shool);
                if (api.Count > 0)
                {
                    api.RemoveAt(0);
                }

                return View(api);

            } catch { return RedirectToAction("Index"); }
           
        }
        public async Task<IActionResult> Save()
        {
            try {
                string cookie = Request.Cookies[UserContant.UseToken];
                if (cookie == null || cookie.Length == 0)
                {
                    return RedirectToAction("Index", "Account");
                }
                string shool = HttpContext.Session.GetString("School");
                var api = JsonConvert.DeserializeObject<List<School>>(shool);
                if (api.Count > 0)
                {
                    api.RemoveAt(0);
                  await service.ListCreate(api);             
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }


          public async Task<IActionResult> DeleteShool(long id)
        {
        
            return Ok(await service.Delete(id));
        }

        public IActionResult Import(string ViewSchool)
        {
            string cookie = Request.Cookies[UserContant.UseToken];
            HttpContext.Session.SetString("School", ViewSchool);
            return Ok("ok");

        }

        public async Task<IActionResult> Update(long id)
        {
            string cookie = Request.Cookies[UserContant.UseToken];

            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
            var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TH, PermisstionConTant.QL_TS });
            if (check)
            {
                ApiResult apiResult =  await service.GetByID(id);
                School School= (School) apiResult.Data;
                List<School> schools = new List<School>();
                schools.Add(School);
                return View(schools);
            }
            return View("UseNotFound");
        }

        public IActionResult Create(string ViewSchool)
        {
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
        
            return View();

        }
    }
}
