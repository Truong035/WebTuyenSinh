using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh_Application.Interface;

namespace WebTuyenSinhAdmin.Controllers
{
    public class UseController : Controller
    {
        private IUserService service;

        public UseController(IUserService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await service.GetAll(1));
        }

        public async Task<IActionResult> Role()
        {
            return View(await service.GetAllRole());
        }

        
        public async Task<IActionResult> RoleCreate(Role Role)
        {
            return Ok(await service.RoleCreate(Role));
        }
        public async Task<IActionResult> Create(User use )
        {
            return Ok(await service.Create(use));
        }
        public async Task<IActionResult> GetRole(long id)
        {
            return Ok(await service.GetRoleByid(id));
        }
        

    }
}
