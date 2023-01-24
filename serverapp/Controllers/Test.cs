using Microsoft.AspNetCore.Mvc;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace serverapp.Controllers
{
    [ApiController]
    public class Test : ControllerBase
    {
        [HttpPost]
        [Route("upload")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancelalationToken)
        {
            await WriteFile(file);
            return Ok();
        }
        private async Task<bool> WriteFile(IFormFile file)
        {
            bool result = false;
            string filename;
            try
            {
                filename = Path.GetFileName(file.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "uploads", filename);
                using (var stream = new FileStream(path, FileMode.Create))  
                {
                    await file.CopyToAsync(stream);
                }
                result = true;
                
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
