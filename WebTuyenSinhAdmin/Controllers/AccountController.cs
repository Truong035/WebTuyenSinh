using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.Modell;
using WebTuyenSinh_Application.ViewApi;
using WebTuyenSinhAdmin.Contan;

namespace WebTuyenSinhAdmin.Controllers
{
    public class AccountController : Controller
    {
        private ILoginService _login;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IValidateTokenService _IValidateTokenService;
        public AccountController(ILoginService login, IHttpContextAccessor httpContextAccessor, IValidateTokenService validateTokenService)
        {
            _login = login;
            this._httpContextAccessor = httpContextAccessor;
            _IValidateTokenService = validateTokenService;
        }
      
       
        public async Task<IActionResult> ResetPassword()
        {
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
            ResetPassword resetPassword =  await _IValidateTokenService.ValidateToken(cookie);
            if (resetPassword != null)
            {
                return View(resetPassword);
            }


            return RedirectToAction("Index", "Account");
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            if (!ModelState.IsValid)
                return View(resetPassword);
            else
            {
                ApiResult apiResult = await _login.ResetPassWordUse(resetPassword);
                if (apiResult.Success)
                {

                    return RedirectToAction("Index", "Account");
                   
                }
                ModelState.AddModelError("Eroll", apiResult.Message);
                return View(resetPassword);
            }

        }
        public IActionResult Index()
        {
    
            Response.Cookies.Delete("Token");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
        {
            if (ModelState.IsValid)
            {
         
                ApiResult apiResult = await _login.LoginUse(request);
                if (apiResult.Success)
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Append(UserContant.UseToken, apiResult.Data.ToString(), option);
                    return RedirectToAction("Index", "Home");

                }
                ModelState.AddModelError(nameof(request.Email), apiResult.Message);
            }
            return View(request);
        }
    }
}
