using AspWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using serverapp.Services;
using System.Collections;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService UserService;
    public UserController(UserService UserService)
    {
        this.UserService = UserService;
    }
    [HttpGet("get-all-users")]
    public async Task<IActionResult> Get()
    {
        var users = await UserService.GetUsersAsync();
        return Ok(users);
    }
    [HttpGet("get-user-by-id/{Id}")]
    public async Task<IActionResult> Get(int Id)
    {
        var users = await UserService.GetUserByIdAsync(Id);
        if (users == null)
            return NotFound("File not found");
        return Ok(users);
    }
    [HttpGet("get-user-by-email-and-password/{email}/{password}")]
    public async Task<IActionResult> Get(string email, string password)
    {
        
        var users = await UserService.GetUserByEmailAndPasswordAsync(email, password);
        if (users == null)
            return NotFound("File not found");
        return Ok(users);
    }
    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser([FromBody] User userToCreate)
    {
        bool createSuccessful = await UserService.CreateUserAsync(userToCreate);

        if (createSuccessful)
        {
            return Ok("Create successful.");
        }
        else
        {
            return BadRequest("not created there is a probleme");
        }
    }
    [HttpPost("create-admin")]
    public async Task<IActionResult> CreateAdmin([FromBody] User userToCreate)
    {
        bool createSuccessful = await UserService.CreateAdminAsync(userToCreate);

        if (createSuccessful)
        {
            return Ok("Create successful.");
        }
        else
        {
            return BadRequest("not created there is a probleme");
        }
    }
    [HttpPut("update-admin")]
    public async Task<IActionResult> UpdateAdmin([FromBody] User employeToUpdate)
    {
        bool updateSuccessful = await UserService.UpdateAdminAsync(employeToUpdate);

        if (updateSuccessful)
        {
            return Ok("Update successful.");
        }
        else
        {
            return BadRequest("not updated there is a probleme");
        }
    }
    [HttpPut("update-user")]
    public async Task<IActionResult> UpdateUser([FromBody] User employeToUpdate)
    {
        bool updateSuccessful = await UserService.UpdateUserAsync(employeToUpdate);

        if (updateSuccessful)
        {
            return Ok("Update successful.");
        }
        else
        {
            return BadRequest("not updated there is a probleme");
        }
    }
    [HttpDelete("delete-user/{Id}")]
    public async Task<IActionResult> UpdateUser(int Id)
    {
        bool deleteSuccessful = await UserService.DeleteUserAsync(Id);

        if (deleteSuccessful)
        {
            return Ok("Delete successful.");
        }
        else
        {
            return BadRequest("not deleted there is a probleme");
        }
    }
}
