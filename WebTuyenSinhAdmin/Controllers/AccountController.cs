using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.Modell;
using WebTuyenSinh_Application.ViewApi;

namespace WebTuyenSinhAdmin.Controllers
{
    public class AccountController : Controller
    {
        private ILoginService _login;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountController(ILoginService login, IHttpContextAccessor httpContextAccessor)
        {
            _login = login;
            this._httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginRequest request)
        {
            if (ModelState.IsValid)
            {
         
                ApiResult apiResult = await _login.Login(request);
                if (apiResult.Success)
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Append("Token", apiResult.ToString(), option);
                    return RedirectToAction("Index", "Home");

                }
                ModelState.AddModelError(nameof(request.Email), apiResult.Message);
            }
            return View(request);
        }
    }
}
