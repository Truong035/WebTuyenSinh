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

namespace WebTuyenSinhAdmin.Controllers
{
    public class ManagerSchoolController : Controller
    {
        private readonly ISchoolService service;
        private readonly IExcellService _IExcellService;
        public ManagerSchoolController(ISchoolService service, IExcellService IExcellService)
        {
            this.service = service;
            _IExcellService = IExcellService;
        }

        public async Task<IActionResult> Index()
        {
       List<School> schools =(List<School>) await  service.GetAll();
            return View(schools);
        }

        public IActionResult ViewExcel()
        {
            try {
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
                string shool = HttpContext.Session.GetString("School");
                var api = JsonConvert.DeserializeObject<List<School>>(shool);
                if (api.Count > 0)
                {
                    api.RemoveAt(0);
                    ApiResult a = await service.ListCreate(api);             
                }
            }
            catch
            {
    
            }
            return RedirectToAction("Index");
        }

        [HttpDelete("{id}")]
          public async Task<IActionResult> DeleteShool(string id)
        {
            return Ok();
        }

        public IActionResult Import(string ViewSchool)
        {
           
            HttpContext.Session.SetString("School", ViewSchool);
            return Ok("ok");

        }
    }
}
