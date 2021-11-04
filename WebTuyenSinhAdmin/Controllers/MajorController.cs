using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.Interface;

namespace WebTuyenSinhAdmin.Controllers
{
    public class MajorController : Controller
    {
        private readonly IManageMajorSevice _sevice;

        public MajorController(IManageMajorSevice sevice)
        {
            _sevice = sevice;
        }

        public async Task<IActionResult> Index()
        {
            return View( await _sevice.GetAll());
        }
        public async Task<IActionResult>  Import(string ViewMajor)
        {
            try {
                var listMajor = JsonConvert.DeserializeObject<List<Major>>(ViewMajor);
                if (listMajor != null)
                {
                    return Ok(await _sevice.CreateList(listMajor));
                }

            } catch { }
         
            return Ok("ok");

        }
    }
}
