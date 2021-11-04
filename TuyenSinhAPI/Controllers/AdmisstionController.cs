using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TuyenSinhAPI.Interface;
using TuyenSinhAPI.Modell;
using TuyenSinhAPI.ViewApi;
using WebTuyenSinh.Data.Entityes;

namespace TuyenSinhAPI.Controllers
{
    [Route("api/v1/utc2/admisstion")]
    [ApiController]
    public class AdmisstionController : ControllerBase
    {
        private readonly IAdmisstionService _AdmisstionService;

        public AdmisstionController(IAdmisstionService admisstionService)
        {
            _AdmisstionService = admisstionService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] AdmisstionCreate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _AdmisstionService.CreateAdmisstion(request);
            return Ok(result);
        }

        [HttpGet("Detail/{id}")]
        public async Task<IActionResult> GetByID(long? id)
        {
            if (id == null)
                return BadRequest(ModelState);
            var result = await _AdmisstionService.GetByID(id);
            return Ok(result);
        }
        

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _AdmisstionService.GetAll();
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmisstion(long? id)
        {
            if (id == null)
                return BadRequest(ModelState);
            var result = await _AdmisstionService.DeleteAdmisstion(id);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmisstion(long? id, [FromBody] AdmisstionCreate update)
        {
            if (id == null)
                return BadRequest(ModelState);
            var result = await _AdmisstionService.UpdateAdmisstion(id, update);
            return Ok(result);
        }
        [HttpPost("admisstion_major/{id}")]
        public async Task<IActionResult> CreateAdmisstion_Major(long? id,[FromBody] List<Admisstion_MajorCreate> request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _AdmisstionService.CreateAdmisstion_Major(request,id);
            return Ok(result);
        }

        [HttpDelete("admisstion_major/{id}")]
        public async Task<IActionResult> DeleteAdmisstion_Major(long? id)
        {
            if (id == null)
                return BadRequest(ModelState);
            var result = await _AdmisstionService.DeleteAdmisstion_Major(id);
            return Ok(result);
        }
      [HttpPut("admisstion_major/{id}")]
        public async Task<IActionResult> UpdateStatus(long? id, Admisstion_MajorUpdateStatus updateStatus)
        {
            if (id == null)
                return BadRequest(ModelState);
            var result = await _AdmisstionService.UpdateStatusAdmisstion_Major(id,updateStatus);
            return Ok(result);
        }
       

    }
}
