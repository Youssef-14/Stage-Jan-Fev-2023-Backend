using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using serverapp.Services;

namespace serverapp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DemandsController : ControllerBase
    {
        private readonly DemandeService DemandeService;
        public DemandsController()
        {
            this.DemandeService = new DemandeService(new AppDBContext());
        }
        [Authorize]
        [HttpGet("get-all-demandes")]
        public async Task<IActionResult> GetAllDemands()
        {
            var demands= await DemandeService.GetDemandesAsync();
            return Ok(demands);
        }
        [Authorize]
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
        [Authorize]
        [HttpGet("get-all-accepted-demands")]
        public async Task<IActionResult> GetAllAcceptedDemands()
        {
            var demands = await DemandeService.GetAcceptedDemandesAsync();
            return Ok(demands);
        }
        [Authorize]
        [HttpGet("get-all-rejected-demands")]
        public async Task<IActionResult> GetAllRefusedDemands()
        {
            var demands = await DemandeService.GetRefusedDemandesAsync();
            return Ok(demands);
        }
        [Authorize]
        [HttpGet("get-all-pending-demands")]
        public async Task<IActionResult> GetAllPendingDemands()
        {
            var demands = await DemandeService.GetPendingDemandesAsync();
            return Ok(demands);
        }
        [Authorize]
        [HttpGet("get-to-be-corrected-demands")]
        public async Task<IActionResult> GetAllToBeCorrectedDemands()
        {
            var demands = await DemandeService.GetToBeCorrectedDemandesAsync();
            return Ok(demands);
        }
        [Authorize]
        [HttpGet("get-filtered-demands/{type}/{status}/{begin}/{end}/{tri}")]
        public async Task<IActionResult> GetAllFilteredDemandesDemands(string type, string status, int begin, int end, string tri)
        {
            var demands = await DemandeService.GetFilteredDemandesAsync(type, status, begin, end, tri);
            return Ok(demands);
        }
        [Authorize]
        [HttpGet("get-demande-by-id/{Id}")]
        public async Task<IActionResult> GetDemandeById(int Id)
        {
            var demande = await DemandeService.GetDemandeByIdAsync(Id);
            return Ok(demande);
        }

        [Authorize]
        [HttpPut("set-demande-to-accepted/{Id}")]
        public async Task<IActionResult> SetDemandeToAccepted(int Id)
        {
            if (await DemandeService.SetDemandeToAcceptedAsync(Id))
            {
                return Ok("Update successful.");
            }
            else
            {
                return BadRequest("not updated there is a probleme");
            }
        }
        [Authorize]
        [HttpPut("set-demande-to-refused/{Id}")]
        public async Task<IActionResult> SetDemandeToRefused(int Id)
        {
            if (await DemandeService.SetDemandeToRefusedAsync(Id))
            {
                return Ok("Update successful.");
            }
            else
            {
                return BadRequest("not updated there is a probleme");
            }
        }
        [Authorize]
        [HttpPut("set-demande-to-pending/{Id}")]
        public async Task<IActionResult> UpdateUser(int Id)
        {
            if (await DemandeService.SetDemandeToPendingAsync(Id))
            {
                return Ok("Update successful.");
            }
            else
            {
                return BadRequest("not updated there is a probleme");
            }
        }
        [Authorize]
        [HttpPut("set-demande-to-be-corrected/{Id}")]
        public async Task<IActionResult> SetDemandeToBeCorrected(int Id)
        {
            if (await DemandeService.SetDemandeToBeCorrectedAsync(Id))
            {
                return Ok("Update successful.");
            }
            else
            {
                return BadRequest("not updated there is a probleme");
            }
        }
        [Authorize]
        // DELETE api/<DemandsController>/5
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
