using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.ViewApi;
using WebTuyenSinhClinet.Models;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.Modell;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace WebTuyenSinhClinet.Controllers
{
  
    public class HomeController : Controller
    {
 
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HomeController> _logger;
        private readonly IAdmisstionService _service;
        public HomeController(ILogger<HomeController> logger , IHttpContextAccessor httpContextAccessor, IAdmisstionService service)
        {
            _logger = logger;
            _service = service;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> ProfileDetail(long? id)
        {
            string uid = _httpContextAccessor.HttpContext.Session.GetString("ID");
            if (uid == null || id == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                ApiResult result = await _service.GetByProfile(id);
                ProfileView view = (ProfileView)result.Data;
                result = await _service.GetByID(view.Data.idAdmisstion);
                Admisstion Admisstion = (Admisstion)result.Data;
                ViewBag.data = Admisstion;
                if (view.Data.Statust == 3 || ((view.Data.CloseTime != null) && (view.Data.CloseTime.Value > DateTime.Now)))
                {
                    if (Admisstion.Type == 1)
                    {
                        return View("FormCapacityEdit", view);
                    }
                    else
                    {
                        return View(view);
                    }
                }
                if (view.Data.Statust == 0)
                {
                    if (Admisstion.Type == 1)
                    {
                        return View("UpdateFormCapacity", view);
                    }
                    else
                    {
                        return View("UpdateProfile", view);
                    }
                }
                if (Admisstion.Type == 1)
                {
                    return View("FormCapacityDetail", view);
                }
                return View("ProfileDetail", view);
            }
            catch (Exception e)
            {
                return View("Error");
            }

            //try
            //{
            //    if (id == null)
            //    {
            //        return View("Error");
            //    }
            //    ApiResult result = await _service.GetByProfile(id);
            //    ProfileView view = (ProfileView)result.Data;
            //    result = await _service.GetByID(view.Data.idAdmisstion);
            //    Admisstion Admisstion = (Admisstion)result.Data;
            //    ViewBag.data = Admisstion;
            //    return View(view);
            //}
            //catch (Exception e)
            //{
            //    return View("Error");
            //}
        }
        public IActionResult ResultProfile(long id)
        {
            ViewBag.url = id;
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Addmission()
        {
            ApiResult result = await _service.GetAll(5);
            List<Admisstion> Admisstion = (List<Admisstion>)result.Data;
            return View(Admisstion);
        }
       

        public async Task<IActionResult> EditProfile(long? id)
        {
            string uid = _httpContextAccessor.HttpContext.Session.GetString("ID");
            if (uid == null || id==null)
            {
                return RedirectToAction("Index");
            }
            try
            {   
                ApiResult result = await _service.GetByProfile(id);
                ProfileView view = (ProfileView)result.Data;
                result = await _service.GetByID(view.Data.idAdmisstion);
                Admisstion Admisstion = (Admisstion)result.Data;
                ViewBag.data = Admisstion;
                if (view.Data.Statust == 3 || ((view.Data.CloseTime != null) && (view.Data.CloseTime.Value > DateTime.Now)))
                {
                    if (Admisstion.Type == 1)
                    {
                        return View("FormCapacityEdit", view);
                    }
                    else
                    {
                        return View(view);
                    }
                }
                if (view.Data.Statust == 0)
                {
                    if (Admisstion.Type == 1)
                    {
                        return View("UpdateFormCapacity", view);
                    }
                    else
                    {
                        return View("UpdateProfile", view);
                    }
                }
                if (Admisstion.Type == 1)
                {
                    return View("FormCapacityDetail", view);
                }
                return View("ProfileDetail", view);
            }
            catch (Exception e)
            {
                return View("Error");
            }
        }
        public async Task<IActionResult> GETProfile(long? id)
        {
            ApiResult result = await _service.GetByProfile(id);
            return Ok(result);
        }
        public async Task<IActionResult> CreateProfile(long? id)
        {
            ApiResult result = new ApiResult();
            string uid = _httpContextAccessor.HttpContext.Session.GetString("ID");
            if (uid == null)
            {
                return RedirectToAction("Login", "Account");
            }
            try
            {  result = await _service.CheckProFile(id.Value, uid);
                if (!result.Success && result.Data!=null && ((ProfileStudent)result.Data).Statust==0)
                {
                    result = await _service.GetByProfile(((ProfileStudent)result.Data).id);
                    return Ok(result);
                }
                result = await _service.CreateProfile(id);
                return Ok(result);
            }
            catch(Exception e){
                return Ok(result);
            }
              
        }
        [HttpPost]
        public async Task<IActionResult> CreateProfile(ProfileStudent id, List<FileProfileView> files)
        {

            ApiResult result = await _service.CreateProfile(id, files);

            return Ok(result);
        }


        public async Task<IActionResult> GetAll(long id)
        {
            string uid = _httpContextAccessor.HttpContext.Session.GetString("ID");
            if (uid == null)
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                ApiResult result = await _service.GetAllProFileStudent(uid);
                if (result.Success)
                {
                    List<ProfileStudentsView> views = (List<ProfileStudentsView>)result.Data;
                    return View(views);
                }
            }
            catch
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
            public async Task<IActionResult> Create(long id)
        {
            string uid = _httpContextAccessor.HttpContext.Session.GetString("ID");
            if (uid == null)
            {
                return RedirectToAction("Login", "Account");
            }
            try {
                ApiResult result = await _service.CheckProFile(id, uid);
                if (result.Success || (result.Data != null && ((ProfileStudent)result.Data).Statust == 0))
                {
                     result = await _service.GetByID(id);
                    Admisstion admisstion = (Admisstion)result.Data;
                if(admisstion==null) return View();
                if (admisstion.Statust == 1) {
                        if (admisstion.Type == 1)
                        {
                            return View("FormCapacity");
                        }
                        return View();
                    }
                    else
                    {
                        return View("expireAdmisstion");
                    }
                }
                if (!result.Success && result.Data != null && ((ProfileStudent)result.Data).Statust !=0 )
                {
                    ViewBag.Url = "/Home/EditProfile/" + ((ProfileStudent)result.Data).id;
                }  
            }
            catch
            {
                return RedirectToAction("Index");
            }
       
            return View("Notification");
        }
        [HttpGet()]
        public async Task<IActionResult> GetAdmisstionInfo(long? id)
        {

            ApiResult result = await _service.GetAdmisstionInfo(id);

            return Ok(result);
        }

 
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
