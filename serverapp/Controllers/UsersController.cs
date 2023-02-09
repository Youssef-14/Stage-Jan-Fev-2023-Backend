using AspWebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using serverapp.Services;
using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService UserService;
    public UserController()
    {
        this.UserService = new UserService(new AppDBContext());
    }
    [Authorize(Roles = "admin")]
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
            return NotFound("User not found");
        return Ok(users);
    }
    [HttpPost("authentificate/{email}/{password}")]
    public async Task<IActionResult> Get(string email, string password)
    {
        var user = await UserService.GetUserByEmailAndPasswordAsync(email, password);
        
        if (user == null)
            return NotFound("User not found");
        user.Token = CreateJwt(user);
        return Ok(new {
            Token = user.Token,
            Message = "Login successful.",
        });
    }
    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser([FromBody] User userToCreate)
    {
        //to be co
        return Ok(await UserService.CreateUserAsync(userToCreate));
    }
    [HttpPost("create-admin")]
    public async Task<IActionResult> CreateAdmin([FromBody] User userToCreate)
    {
        ////to be co
        return Ok( await UserService.CreateAdminAsync(userToCreate));
    }
    [Authorize(Roles ="Admin")]
    [HttpPut("update-password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordModel pass)
    {
        //to be co
        if (await UserService.UpdatePasswordAsync(pass))
        {
            return Ok("Update successful.");
        }
        else
        {
            return BadRequest("not updated there is a probleme");
        }
    }
    [Authorize]
    [HttpPut("update-user")]
    public async Task<IActionResult> UpdateUser([FromBody] User employeToUpdate)
    {
        if (await UserService.UpdateUserAsync(employeToUpdate))
        {
            return Ok("Update successful.");
        }
        else
        {
            return BadRequest("not updated there is a probleme");
        }
    }
    [HttpDelete("delete-user/{Id}")]
    public async Task<IActionResult> DeleteUser(int Id)
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
    //JWT
    private string CreateJwt(User user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("veryverysecret.......");
        var identity = new ClaimsIdentity(new Claim[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Type)
        });
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = credentials
        };
        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        return jwtTokenHandler.WriteToken(token);
    }
}
