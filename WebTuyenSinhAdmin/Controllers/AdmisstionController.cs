using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.Interface;
using WebTuyenSinh_Application.Modell;
using WebTuyenSinh_Application.Repository;
using WebTuyenSinh_Application.System;
using WebTuyenSinh_Application.ViewApi;
using WebTuyenSinhAdmin.Contan;

namespace WebTuyenSinhAdmin.Controllers
{
    public class AdmisstionController : Controller
    {
        private readonly IValidateTokenService _IValidateTokenService;
        private readonly IAdmisstionService _service;
        private readonly IManageMajorSevice manageMajor;
        private readonly IStorageService _storage;
        public AdmisstionController(IAdmisstionService service, IManageMajorSevice majorSevice , IStorageService storage, IValidateTokenService validateTokenService)
        {
            _service = service;
            manageMajor = majorSevice;
            _storage = storage;
            _IValidateTokenService = validateTokenService;
        }
        
        public async Task<IActionResult> GETProfile ( long? id)
            {
            ApiResult result = await _service.GetByProfile(id);
 
            return Ok(result);
        }
              public async Task<IActionResult> ProfileDetail(long? id)
        {
            try
            {
                string cookie = Request.Cookies[UserContant.UseToken];
                if (cookie == null || cookie.Length == 0)
                {
                    return RedirectToAction("Index", "Account");
                }
                var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TS, PermisstionConTant.QL_TH });
              
                if (id == null)
                {
                    return View("Error");
                }
                ApiResult result=await   _service.GetByProfile(id);
                ProfileView view = (ProfileView) result.Data;
          
                result = await _service.GetByID(view.Data.idAdmisstion);
                Admisstion Admisstion = (Admisstion)result.Data;
                ViewBag.data = Admisstion;
                if (!check)
                {
                    return View("UseNotFound");
                   
                }
                if (Admisstion.Type == 1)
                {
                    return View(view);
                   
                }
                else
                {
                    return View("Test", view);
                }
              
            }
            catch(Exception e)
            {
                return View("Error");
            }
        }
        
        [HttpPost()]
        public async Task<IActionResult> Dowload(long? id , int Type)
        {

            ApiResult result = await _service.DowloadFile(id, Type);

            return Ok(result);
        }
        [HttpGet()]
        public async Task<IActionResult> GetAdmisstionInfo(long? id)
        {
        
            ApiResult result = await _service.GetAdmisstionInfo(id);
         
            return Ok(result);
        }
        [HttpPost()]
        public async Task<IActionResult> CreateProfile(ProfileStudent Profile,List<FileProfileView> files)
        {
            return Ok(await _service.CreateProfile(Profile, files));
        }
        public async Task<IActionResult> ListAll(long? id ,string ShoolName, int? page= 0)
        {
            try
            {
                string cookie = Request.Cookies[UserContant.UseToken];
                if (cookie == null || cookie.Length == 0)
                {
                    return RedirectToAction("Index", "Account");
                }
                if (id == null)
                {
                    return View("Error");
                }
                ApiResult result = await _service.ListAll(id);
                List<ProfileStudentsView> view = (List<ProfileStudentsView>)result.Data;
                ViewBag.Majo = result.Message;
              
                if (ShoolName != null && ShoolName.Length > 0) { view = view.Where(x => x.CMND.Contains(ShoolName) ||x.Teletephone.Contains(ShoolName) || x.Name.Contains(ShoolName) || x.id.ToString().Contains(ShoolName)  ).ToList(); }
                else
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
                    page = 0;
                }
                start = (int)(page - 1) * limit;
                ViewBag.pageCurrent = page;
                ViewBag.Search = ShoolName;
                int totalProduct = view.Count;
                float numberpage = totalProduct / limit;
                ViewBag.numberPage = (int)Math.Ceiling(numberpage);        
                return View(view.Skip(start).Take(limit).ToList());
            }
            catch
            {
                return View("Error");
            }
        }
            public async Task<IActionResult> ListProfile(long? id)
        {
            try
            {
                string cookie = Request.Cookies[UserContant.UseToken];
                if (cookie == null || cookie.Length == 0)
                {
                    return RedirectToAction("Index", "Account");
                }
                if (id == null)
                {
                    return View("Error");
                }
                ApiResult result = await _service.ListProfile(id);
             List<ProfileStudentsView> view = (List<ProfileStudentsView>)result.Data;
                ViewBag.Majo = result.Message;
                return View(view);
            }
            catch
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Detail(long? id )
        {
            try {
                string cookie = Request.Cookies[UserContant.UseToken];
                if (cookie == null || cookie.Length == 0)
                {
                    return RedirectToAction("Index", "Account");
                }
                if (id == null)
                {
                    return View("Error");
                }
                ApiResult result = await _service.GetByID(id);
                Admisstion Admisstion = (Admisstion)result.Data;
                return View(Admisstion);
            }  catch { return View("Error");
    }
        }

        
          public async Task<IActionResult> ListAllFile()
        {
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
            var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TS, PermisstionConTant.QL_FILE });
            if (check)
            {
                var path1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Profile");
                // 2.Khai báo một thể hiện của lớp DirectoryInfo
                DirectoryInfo directory = new DirectoryInfo(path1);
                
                ViewBag.File = directory.GetFiles().Count();
                return View();
            }
            return View("UseNotFound");
        }

        public async Task<IActionResult> Block()
        {

            ApiResult result = await _service.GetBockAll();
            List<Block> Block = (List<Block>)result.Data;
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
            var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TS, PermisstionConTant.QL_TH});
            if (check)
            {
                return View(Block);
            }
            return View("BlockUse",Block);
        }
        public async Task<IActionResult> Index()
        {
            ApiResult result = await _service.GetAll(4);
            List<Admisstion> Admisstion = (List<Admisstion>)result.Data;
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
            var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TS });
            if (check)
            {
                return View(Admisstion);
            }
            return View("IndexUse",Admisstion);

        }
        [HttpGet]
        public async Task<IActionResult> Result()
        {
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
            ApiResult result = await _service.GetAll(4);
            List<Admisstion> Admisstion = (List<Admisstion>)result.Data;
           
          
            return View(Admisstion);
        }
        
         [HttpGet]
        public async Task<IActionResult> Export(long? id)
        {

            ApiResult result = await _storage.ExportExcell(id);

            return Ok(result);

        }
        [HttpGet]
        public async Task<IActionResult> ExportExcellOne(long? id)
        {

            ApiResult result = await _storage.ExportExcellOne(id);

            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> GetResult(long? id)
        {

               
                ApiResult result = await _service.GetProfiles(id);

                return Ok(result);
            
         
        }
        [HttpGet]
        public async Task<IActionResult> ListAllProfile(long? id, int? page ,string Search)
        {

            try
            {
                string cookie = Request.Cookies[UserContant.UseToken];
                if (cookie == null || cookie.Length == 0)
                {
                    return RedirectToAction("Index", "Account");
                }
                if (id == null)
                {
                    return View("Error");
                }
                ApiResult result = await _service.ListAll(id);
                List<ProfileStudentsView> view = (List<ProfileStudentsView>)result.Data;
                int limit = 20;
                int start;
                if (page== null|| page == 0)
                {
                    page = 1;
                }
                if (!string.IsNullOrEmpty(Search))
                {
                    view = view.Where(x => x.Name.Contains(Search) || x.CMND.Contains(Search) || (x.CMND + "_" + x.Name).Contains(Search)).ToList();
                }
                else
                {
                    Search = "";
                }

                start = (int)(page - 1) * limit;
                ViewBag.pageCurrent = page;
                ViewBag.ID = id.ToString();
             
                float numberpage = view.Count / limit;
                ViewBag.numberPage = (int)Math.Ceiling(numberpage);
                view= view.Skip(start).Take(limit).ToList();
                ViewBag.search = Search;
                ViewBag.Majo = result.Message;

                return View(view);
            }
            catch
            {
                return View("Error");
            }

        }

        
        [HttpPost]
        public async Task<IActionResult> Result(long? id)
        {
          try
            {
                string cookie = Request.Cookies[UserContant.UseToken];
                if (cookie == null || cookie.Length == 0)
                {
                    return RedirectToAction("Index", "Account");
                }
                if (id == null)
                {
                    return View("Error");
                }
 
                ApiResult result = await _service.ListAll(id);
                List<ProfileStudentsView> view = (List<ProfileStudentsView>)result.Data;
                ViewBag.Majo = result.Message;

                return View(view);
            }
            catch
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> EditResult(long? id)
        {
            try {
                string cookie = Request.Cookies[UserContant.UseToken];
                if (cookie == null || cookie.Length == 0)
                {
                    return RedirectToAction("Index", "Account");
                }

                ApiResult result = await _service.GetByID(id);
                Admisstion Admisstion = (Admisstion)result.Data;
                if (Admisstion.Statust != 3)
                {
                    return View("Error");
                }
                result = await _service.GetData();
                AdmisstionData data = (AdmisstionData)result.Data;
                ViewBag.data = data;
                return View(Admisstion);

            } catch { return View("Error"); } 

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
            var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TS});
            if (!check)
            {
                return View("UseNotFound");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
     
            return Ok(await _service.GetData());
        }

        
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(long? id, string comment, DateTime? date,int? status)
        {
            if (id == null)
            {
                return Ok(new ApiResult() { Data = null, Message = "Id không được để trống", Success = false }); ;
            }
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
            var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.KD_HS,PermisstionConTant.QL_TS});
            if (check)
            {
                return Ok(await _service.UpdateStatusProfile(id, comment, date, status));
            }
            return Ok(new ApiResult() { Data = null, Message = "Bạn không được admin cấp quyền ", Success = false }); ;
        }

        public async  Task<IActionResult> ImportExcel()
        {
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
            ApiResult result = await _service.GetAll(4);
            List<Admisstion> Admisstion = (List<Admisstion>)result.Data;
            return View(Admisstion);

        }

        public async Task<IActionResult> ManageProfile()
        {
            ApiResult result = await _service.GetAll(4);
            List<Admisstion> Admisstion = (List<Admisstion>)result.Data;
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
            var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TS });
            if (check)
            {
                return View(Admisstion);
            }
            return View("UseNotFound", Admisstion);

        }


        [HttpPost]
        public async Task<IActionResult> updateStatus(long? id, AdmisstionCreate AdmisstionCreate)
        {
            if (id == null)
            {
                return Ok(new ApiResult() { Data = null,Message="Id không được để trống",Success=false}) ;;
            }
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null && cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
            var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.QL_TS});
            if (check)
            {
                return Ok(await _service.UpdateStatusAdmisstion(id, AdmisstionCreate));
            }
            return Ok(new ApiResult() { Data = null, Message = "Bạn không được admin cấp quyền ", Success = false }); ;
        }
       
        public async Task<IActionResult> Edit(long? id)
        {
            string cookie = Request.Cookies[UserContant.UseToken];
            if (cookie == null || cookie.Length == 0)
            {
                return RedirectToAction("Index", "Account");
            }
            try {
                ApiResult result = await _service.GetByID(id);
                Admisstion Admisstion = (Admisstion)result.Data;
                result = await _service.GetData();
                AdmisstionData data = (AdmisstionData)result.Data;
                ViewBag.data = data;
                if (Admisstion.Statust >2)
                {
                    return View("Error");
                }
                return View(Admisstion);
            } catch
            {
                return View("Error");
            }
          
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdmisstionCreate AdmisstionCreate, List<Block> blocknew, List<Major> majorsnew)
        {
           if(blocknew!=null && blocknew.Count>0)
            {
               await  _service.CreateListBlock(blocknew);
            }
            if (majorsnew != null && majorsnew.Count > 0)
            {
                await manageMajor.CreateList(majorsnew);
            }
            return Ok(await _service.CreateAdmisstion(AdmisstionCreate));
        }

        [HttpPost]
        public async Task<IActionResult> update( long?id, AdmisstionCreate AdmisstionCreate, List<Block> blocknew, List<Major> majorsnew)
        {
            if (id == null)
            {
                return Ok("ok");
            }
            if (blocknew != null && blocknew.Count > 0)
            {
                await _service.CreateListBlock(blocknew);
            }
            if (majorsnew != null && majorsnew.Count > 0)
            {
                await manageMajor.CreateList(majorsnew);
            }

            return Ok(await _service.UpdateAdmisstion(id,AdmisstionCreate));
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
                return Ok(new ApiResult() { Message = "Dữ liệu trống", Data = null, Success = false });
            }
            catch(Exception e) { return Ok(new ApiResult() {Message=e.Message,Data=null,Success=false}); }
        }
        
        public async Task<IActionResult> DeleteBlock(string id)
        {
            try
            {
                    return Ok(await _service.DeleteBlock(id));
            }
            catch (Exception e) { return Ok(new ApiResult() { Message = e.Message, Data = null, Success = false }); }
        }
        public async Task<IActionResult> DeleteAdmisstion(long? id)
        {
            try
            {
                return Ok(await _service.DeleteAdmisstion(id));
            }
            catch (Exception e) { return Ok(new ApiResult() { Message = e.Message, Data = null, Success = false }); }
        }
        [HttpPost]
        public async Task<IActionResult> ImportExcel(long? id, string str)
        {
            try
            {
                string cookie = Request.Cookies[UserContant.UseToken];
                if (cookie == null || cookie.Length == 0)
                {
                    return RedirectToAction("Index", "Account");
                }
                var check = await _IValidateTokenService.ValidateToken(cookie, new List<long>() { PermisstionConTant.KD_HS, PermisstionConTant.QL_TS });
                if (check)
                {
                    var data = JsonConvert.DeserializeObject<List
                        <ImPortExcelTypeOne>>(str);

                    var ApiResult = await _service.ImportExcel(id, data);
                    return Ok(new ApiResult() { Data = ApiResult, Message = "Danh sách file import ", Success = true }); ;

                };
                return Ok(new ApiResult() { Data = null, Message = "Bạn không được admin cấp quyền ", Success = false }); ;


            }
            catch (Exception e) { return Ok(new ApiResult() { Message = e.Message, Data = null, Success = false }); }
        }
        
    }
}
