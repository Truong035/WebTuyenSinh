using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.Modell;
using WebTuyenSinh_Application.Repository;
using WebTuyenSinh_Application.ViewApi;

namespace WebTuyenSinhAdmin.Controllers
{
    public class AdmisstionController : Controller
    {
        private readonly IAdmisstionService _service;

        public AdmisstionController(IAdmisstionService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Block()
        {
            ApiResult result = await _service.GetBockAll();
            List<Block> Block = (List<Block>)result.Data;
            return View(Block);
        }
        public async Task<IActionResult> Index()
        {
            ApiResult result = await _service.GetAll();
            List<Admisstion> Admisstion = (List<Admisstion>)result.Data;
            return View(Admisstion);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
     
            return Ok(await _service.GetData());
        }
       
    
        public async Task<IActionResult> Edit(long id)
        {
            ApiResult result = await _service.GetByID(id);
            Admisstion Admisstion = (Admisstion)result.Data;
           result = await _service.GetData();
            AdmisstionData data = (AdmisstionData)result.Data;
            ViewBag.data = data;
            return View(Admisstion);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdmisstionCreate AdmisstionCreate)
        {
           
          
            return Ok(await _service.CreateAdmisstion(AdmisstionCreate));
        }
        public async Task<IActionResult> Import(string ViewBlock)
        {
            try
            {
                var Blocks = JsonConvert.DeserializeObject<List<Block>>(ViewBlock);
                if (Blocks != null)
                {
                    return Ok(await _service.CreateListBlock(Blocks));
                }

            }
            catch { }

            return Ok("ok");

        }

    }
}
