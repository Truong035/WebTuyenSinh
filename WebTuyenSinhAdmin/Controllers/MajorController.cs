using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.ViewApi;
using WebTuyenSinhAdmin.Contan;

namespace WebTuyenSinhAdmin.Controllers
{
    public class MajorController : Controller
    {
        private readonly IManageMajorSevice _sevice;
        private readonly IValidateTokenService _IValidateTokenService;
        public MajorController(IManageMajorSevice sevice, IValidateTokenService IValidateTokenService)
        {
            _sevice = sevice;
            _IValidateTokenService = IValidateTokenService;

        }
        public async Task<IActionResult> Delete(string id)
        {

            return Ok(await _sevice.Delete(id));
        }
        public async Task<IActionResult> Index()
        {
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
            var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_NG });
            if (check)
            {
                return View(await _sevice.GetAll());
            }
            return View("IndexUse", await _sevice.GetAll());
  
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
        public async Task<IActionResult> GETALL()
        {
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return Ok(new ApiResult() {Message="Bạn vui lòng đăng nhập"  ,Success=false});
            }
            var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_NG });
            if (check)
            {
                return Ok(new ApiResult() { Data = await _sevice.GetAll(), Message = "Get All" ,Success=true});
            }

            return Ok(new ApiResult() { Message = "Bạn vui lòng đăng nhập", Success = false });
        }
    }
}
