using Microsoft.AspNetCore.Mvc;
using System.IO;


namespace serverapp.Controllers
{
    [ApiController]
    public class Test : ControllerBase
    {
        [HttpPost]
        [Route("upload/{dossier}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadFile(string dossier,IFormFile file)
        {
            await WriteFile(dossier,file);
            return Ok();
        }
        [HttpPost]
        [Route("uploads/{dossier}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UploadFiles(string dossier, List<IFormFile> files)
        {
            foreach (var file in files)
            {
                await WriteFile(dossier, file);
            }
            return Ok();
        }
        private async Task<bool> WriteFile(string dossier,IFormFile file)
        {
            bool result = false;
            string filename;
            try
            {
                filename = Path.GetFileName(file.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "uploads\\"+dossier);

                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var stream = new FileStream(Path.Combine(path, filename) , FileMode.Create))  
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
        

        [HttpGet]
        [Route("download/{dossier}/{filename}")]
        public IActionResult DownloadFile(string dossier, string filename)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads\\" + dossier, filename);
            var fileStream = new FileStream(filePath, FileMode.Open);
            return File(fileStream, "application/octet-stream", filename);
        }
        [HttpGet]
        [Route("getfiles/{dossier}")]
        public IActionResult GetFiles(string dossier)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads\\" + dossier);
            var fileNames = Directory.GetFiles(directoryPath);
            return Ok(fileNames);
        }
        [HttpDelete]
        [Route("delete/{dossier}/{filename}")]
        public IActionResult DeleteFile(string dossier, string filename)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads\\" + dossier, filename);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                return Ok("File deleted successfully");
            }
            else
            {
                return NotFound("File not found");
            }
        }

    }
}
