using Microsoft.AspNetCore.Mvc;
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
    public class UseController : Controller
    {
        private IUserService service;
        private readonly IValidateTokenService _IValidateTokenService;
        public UseController(IUserService service, IValidateTokenService IValidateTokenService)
        {
            this.service = service;
            _IValidateTokenService = IValidateTokenService;
        }

        public async Task<IActionResult> Index()
        {
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
            var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TK });
            if (check)
            {
                return View(await service.GetAll(1));     
            }
            return View("UseNotFound");
           
        }

        public async Task<IActionResult> Role()
        {
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
            var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TK });
            if (check)
            {
                return View(await service.GetAllRole());
            }
            return View("UseNotFound");
        }

        
        public async Task<IActionResult> RoleCreate(Role Role)
        {
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return Ok(new ApiResult() { Message = "Vui lòng đăng nhập", Data = null, Success = false });
                
            }
            var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TK });
            if (check)
            {
                return Ok(await service.RoleCreate(Role));
               
            }
            return Ok(new ApiResult() {Message="Bạn không đc admin cấp quyền",Data=null,Success=false});
        }
        public async Task<IActionResult> Create(User use )
        {
      
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return Ok(new ApiResult() { Message = "Vui lòng đăng nhập", Data = null, Success = false });

            }
            var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TK });
            if (check)
            {
                return Ok(await service.Create(use));

            }
            return Ok(new ApiResult() { Message = "Bạn không đc admin cấp quyền", Data = null, Success = false });
        }
        public async Task<IActionResult> GetRole(long id)
        {
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return Ok(new ApiResult() { Message = "Vui lòng đăng nhập", Data = null, Success = false });

            }
            var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TK });
            if (check)
            {
                return Ok(await service.GetRoleByid(id));

            }
            return Ok(new ApiResult() { Message = "Bạn không đc admin cấp quyền", Data = null, Success = false });
       
        }

        public async Task<IActionResult> DeleteRole(long id)
        {
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return Ok(new ApiResult() { Message = "Vui lòng đăng nhập", Data = null, Success = false });

            }
            var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TK });
            if (check)
            {
                return Ok(await service.DeleteRole(id));

            }
            return Ok(new ApiResult() { Message = "Bạn không đc admin cấp quyền", Data = null, Success = false });

        }
        public async Task<IActionResult> DeleteUse(long id)
        {
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return Ok(new ApiResult() { Message = "Vui lòng đăng nhập", Data = null, Success = false });

            }
            var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TK });
            if (check)
            {
                return Ok(await service.DeleteUse(id));

            }
            return Ok(new ApiResult() { Message = "Bạn không đc admin cấp quyền", Data = null, Success = false });

        }


    }
}
