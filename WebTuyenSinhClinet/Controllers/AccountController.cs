using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using WebTuyenSinhClient.Models;
using WebTuyenSinhClient.Email;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.Modell;
using WebTuyenSinh_Application.ViewApi;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebTuyenSinhClient.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<Account> userManager;
        private SignInManager<Account> signInManager;
        private ILoginService _login;
        private readonly IConfiguration _configuration;
        public AccountController( IConfiguration configuration, UserManager<Account> userMgr, SignInManager<Account> signinMgr , ILoginService login)
        {
            userManager = userMgr;
            signInManager = signinMgr;
            _login = login;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
      
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            Login login = new Login();
            login.ReturnUrl = returnUrl;
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                LoginRequest request = new LoginRequest();
                request.Email = login.Email;
                request.Password = login.Password;
           ApiResult apiResult  =   await  _login.Login(request);
                if (apiResult.Success)
                {
                    await saveToken(apiResult);

                    return RedirectToAction("Index", "Home");

                }
                ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email or password");
            }
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (ModelState.IsValid)
            {
               ApiResult result = await _login.Register(request);
                if (result.Success)
                {

                    await saveToken(result);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View("GoogleResponse", request);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
          
            return View();
        }


        [AllowAnonymous]
        public async Task<IActionResult> LoginTwoStep(string email, string returnUrl)
        {
            var user = await userManager.FindByEmailAsync(email);

            var token = await userManager.GenerateTwoFactorTokenAsync(user, "Email");

            EmailHelper emailHelper = new EmailHelper();
            bool emailResponse = emailHelper.SendEmailTwoFactorCode(user.Email, token);

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginTwoStep(TwoFactor twoFactor, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(twoFactor.TwoFactorCode);
            }

            var result = await signInManager.TwoFactorSignInAsync("Email", twoFactor.TwoFactorCode, false, false);
            if (result.Succeeded)
            {
                return Redirect(returnUrl ?? "/");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Login Attempt");
                return View();
            }
        }

        [AllowAnonymous]

        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("ID");
            HttpContext.Session.Remove("NAME");
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult GoogleLogin()
        {
            string redirectUrl = Url.Action("GoogleResponse", "Account");
            var properties = signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        [AllowAnonymous]
        public async Task<IActionResult> GoogleResponse()
        {
            RegisterRequest register = new RegisterRequest();
            try {
            
                ExternalLoginInfo info = await signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                    return RedirectToAction(nameof(Login));
                register.Email = info.Principal.FindFirst(ClaimTypes.Email).Value;
                register.UseName = info.Principal.FindFirst(ClaimTypes.Name).Value;

                string email = info.Principal.FindFirst(ClaimTypes.Email).Value;
                ApiResult ApiResult = await _login.LoginApi(email);
                if (ApiResult.Success)
                {
                   
                    await saveToken(ApiResult);
                    
                    return RedirectToAction("Index", "Home");
                }
               
              return  View(register);
            } catch {
                return View(register);
            }
          
        }
        [HttpPost]
        public async Task<IActionResult> GoogleResponse(Account account)
        {
            if (ModelState.IsValid)
            {

            }
            return Ok("");
        }
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([Required]string email)
        {
            if (!ModelState.IsValid)
                return View(email);

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var link = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);

            EmailHelper emailHelper = new EmailHelper();
            bool emailResponse = emailHelper.SendEmailPasswordReset(user.Email, link);

            if (emailResponse)
                return RedirectToAction("ForgotPasswordConfirmation");
            else
            {
                // log email failed 
            }
            return View(email);
        }

        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        public async Task saveToken(ApiResult api)
        {
            var userPrincipal = this.ValidateToken((string)api.Data, "ID");
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, authProperties);
            HttpContext.Session.SetString("Token", (string)api.Data);
          
        
        }
        private ClaimsPrincipal ValidateToken(string jwtToken,string Type)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Tokens:Issuer"];
            validationParameters.ValidIssuer = _configuration["Tokens:Issuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);
            var id = principal.Claims.SingleOrDefault(x => x.Type == "ID");
         var name=   principal.Identity.Name;
         //   var name = principal.Claims.SingleOrDefault(x => x.Type == "NAME");
            HttpContext.Session.SetString("ID", id.Value.ToString());
           HttpContext.Session.SetString("NAME", name.ToString());
            return principal ;
        }

        //[AllowAnonymous]
        //public IActionResult ResetPassword(string token, string email)
        //{
        //    var model = new ResetPassword { Token = token, Email = email };
        //    return View(model);
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        //{
        //    if (!ModelState.IsValid)
        //        return View(resetPassword);

        //    var user = await userManager.FindByEmailAsync(resetPassword.Email);
        //    if (user == null)
        //        RedirectToAction("ResetPasswordConfirmation");

        //    var resetPassResult = await userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
        //    if (!resetPassResult.Succeeded)
        //    {
        //        foreach (var error in resetPassResult.Errors)
        //            ModelState.AddModelError(error.Code, error.Description);
        //        return View();
        //    }

        //    return RedirectToAction("ResetPasswordConfirmation");
        //}

        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}