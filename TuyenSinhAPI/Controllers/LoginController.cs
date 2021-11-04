using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TuyenSinhAPI.Modell;
using TuyenSinhAPI.ViewApi;
using WebTuyenSinh.Data.Entityes;
using WebTuyenSinh.Interface;


namespace TuyenSinhAPI.Controllers
{
     [Route("api/v1/utc2/")]
    [ApiController]
 
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _userService;

        public LoginController(ILoginService userService)
        {
            _userService = userService;
      
        }
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.Login(request);
            return Ok(result);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _userService.Register(request);
            return Ok(result);
        }
    }


}
