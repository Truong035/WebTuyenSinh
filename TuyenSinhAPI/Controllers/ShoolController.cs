using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuyenSinhAPI.Interface;
using TuyenSinhAPI.Modell;

namespace TuyenSinhAPI.Controllers
{
    [Route("api/v1/utc2/school")]
    [ApiController]
    public class ShoolController : ControllerBase
    {
        private readonly ISchoolService _school;

        public ShoolController(ISchoolService school)
        {
            _school = school;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
         
            var result = await _school.GetAll();
            return Ok(result);
        }


        [HttpPost("list/Create")]
        public async Task<IActionResult> Create([FromBody] List<ExcellView> request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _school.ListCreate(request);
            return Ok(result);
        }



    }
}
