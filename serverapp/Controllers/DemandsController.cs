using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace serverapp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DemandsController : ControllerBase
    {
        [Authorize(Roles = "admin")]
        [HttpGet("get-all-demandes")]
        public async Task<IActionResult> GetAllDemands()
        {
            var demands= await DemandeService.GetDemandesAsync();
            return Ok(demands);
        }
        [Authorize(Roles = "admin")]
        // GET api/<DemandsController>/5
        [HttpGet("get-demandes-filtered-number/{type}/{status}")]
        public async Task<int> GetFilteredDemandsNumber(string type, string status)
        {
            return await DemandeService.GetDemandesFilteredNumber(type, status);
        }
        [Authorize]
        [HttpGet("get-all-demandes-by-user/{Id}")]
        public async Task<IActionResult> GetAllDemandsById(int Id)
        {
            var demands = await DemandeService.GetDemandsByUserIdAsync(Id);
            return Ok(demands);
        }
        [Authorize(Roles = "admin")]
        [HttpGet("get-all-accepted-demands")]
        public async Task<IActionResult> GetAllAcceptedDemands()
        {
            var demands = await DemandeService.GetAcceptedDemandesAsync();
            return Ok(demands);
        }
        [Authorize(Roles = "admin")]
        [HttpGet("get-all-rejected-demands")]
        public async Task<IActionResult> GetAllRefusedDemands()
        {
            var demands = await DemandeService.GetRefusedDemandesAsync();
            return Ok(demands);
        }
        [Authorize(Roles = "admin")]
        [HttpGet("get-all-pending-demands")]
        public async Task<IActionResult> GetAllPendingDemands()
        {
            var demands = await DemandeService.GetPendingDemandesAsync();
            return Ok(demands);
        }
        [Authorize(Roles = "admin")]
        [HttpGet("get-to-be-corrected-demands")]
        public async Task<IActionResult> GetAllToBeCorrectedDemands()
        {
            var demands = await DemandeService.GetToBeCorrectedDemandesAsync();
            return Ok(demands);
        }
        [Authorize(Roles = "admin")]
        [HttpGet("get-filtered-demands/{type}/{status}/{begin}/{end}/{tri}")]
        public async Task<IActionResult> GetAllFilteredDemandesDemands(string type, string status, int begin, int end, string tri)
        {
            var demands = await DemandeService.GetFilteredDemandesAsync(type, status, begin, end, tri);
            return Ok(demands);
        }
        [Authorize(Roles = "admin")]
        [HttpGet("get-demande-by-id/{Id}")]
        public async Task<IActionResult> GetDemandeById(int Id)
        {
            var demande = await DemandeService.GetDemandeByIdAsync(Id);
            return Ok(demande);
        }
        [HttpPost("create-demande/{UserId}/{type}/{comment}")]
        public async Task<IActionResult> CreateDemande( int UserId,string type, string comment , List<IFormFile> files)
        {
            Demande demande = new Demande();
            demande.UserId = UserId;
            demande.type = type;
            demande.Comment = comment;
            demande.Status = "pending";
            demande.Date = DateTime.Now;

            int result = await DemandeService.CreateDemandeAsync(demande);
            FileController FileController = new FileController();
            await FileController.UploadFiles(result.ToString(), files);
            if (result >0)
            {
                return Ok("Demande created successfully.");
            }
            else if (result == -2)
            {
                return BadRequest("Demande exist deja.");
            }
            else if (result == -1)
            {
                return BadRequest("Type non valide.");
            }
            else
            {
                return BadRequest("Demande no créée. Probleme de Serveur.");
            }
        }
        

        [Authorize(Roles = "admin")]
        [HttpPut("set-demande-to-accepted/{Id}/{AdminId}")]
        public async Task<IActionResult> SetDemandeToAccepted(int Id,int AdminId)
        {
            if (await DemandeService.SetDemandeToAcceptedAsync(Id,AdminId))
            {
                return Ok("Update successful.");
            }
            else
            {
                return BadRequest("not updated there is a probleme");
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPut("set-demande-to-refused/{Id}/{AdminId}")]
        public async Task<IActionResult> SetDemandeToRefused(int Id, int AdminId)
        {
            if (await DemandeService.SetDemandeToRefusedAsync(Id, AdminId))
            {
                return Ok("Update successful.");
            }
            else
            {
                return BadRequest("not updated there is a probleme");
            }
        }
        [HttpPut("set-demande-to-pending/{Id}")]
        public async Task<IActionResult> SetDemandeToRefused(int Id, List<IFormFile> files)
        {

            FileController FileController = new FileController();
            await FileController.UploadFiles(Id.ToString(), files);

            if (await DemandeService.SetDemandeToPendingAsync(Id))
            {
                return Ok("Update successful.");
            }
            else
            {
                return BadRequest("not updated there is a probleme");
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPut("set-demande-to-be-corrected/{Id}/{AdminId}")]
        public async Task<IActionResult> SetDemandeToBeCorrected(int Id, int AdminId)
        {
            if (await DemandeService.SetDemandeToBeCorrectedAsync(Id, AdminId))
            {
                return Ok("Update successful.");
            }
            else
            {
                return BadRequest("not updated there is a probleme");
            }
        }
        [Authorize]
        [HttpDelete("delete-demande-by-id/{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            bool deleteSuccessful = await DemandeService.DeleteDemandeAsync(Id);

            if (deleteSuccessful)
            {
                return Ok("Delete successful.");
            }
            else
            {
                return BadRequest();
            }
        }
        
    }
}
