using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TuyenSinhAPI.Interface;

namespace TuyenSinhAPI.Controllers
{
    [Route("api/v1/utc2/handlefile")]
    [ApiController]
    public class handleFileController : ControllerBase
    {
        private IExcellService _IExcellService;

        public handleFileController(IExcellService iExcellService)
        {
            _IExcellService = iExcellService;
        }

        [HttpPost("Import")]
        public async Task<IActionResult> Import(IFormFile formFile)
        {
            
                //Request.Form.Files[0];
        //    if (file.Length > 0)
        //    {
              var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName.Trim('"');
                return Ok(await _IExcellService.ImportAsync(formFile.OpenReadStream(), fileName));
         //   }
          //  else
          //  {
          //      return BadRequest();
         //   }
        
        }
    }
}
