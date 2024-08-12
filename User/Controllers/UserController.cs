using Microsoft.AspNetCore.Mvc;
using User.Models;

namespace User.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private IUserFactory _userFactory;

    public UserController(IUserFactory userFactory)
    {
        _userFactory = userFactory;
    }

    [HttpGet("/GetAllUsers", Name = "GetAllUsers")]
    public async Task<List<Models.User>> GetAllUsers()
    {
        var users = await _userFactory.GetUserList();

        if (users == null)
        {
            StatusCode(500);
        }
        return users;
    }
    
    [HttpPost("/AddUser", Name = "AddUser")]
    public async Task<IActionResult> AddUser(Models.User user)
    {
        if (user == null)
        {
            return StatusCode(400, "CodeContent cannot be null");
        }

        var guidId = Guid.NewGuid();
        user.Id = guidId;
        await _userFactory.AddUser(user);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _userFactory.GetUserById(id);
        if (user == null)
        {
            StatusCode(500);
            return NotFound();
        }
        return Ok(user);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCodeContent(string id, Models.User user)
    {
        await _userFactory.UpdateUser(user);
        return Ok();
    }
    
    [HttpDelete("DeleteUser/{id}")]
    public async Task<IActionResult> DeleteCodeContent(string id)
    {
        var results = _userFactory.DeleteUser(id);
        return Ok();
    }
}