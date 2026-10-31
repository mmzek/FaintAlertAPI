using Microsoft.AspNetCore.Mvc;
using FaintAlertAPI.DTOs.User;
using FaintAlertAPI.Contracts;
using FaintAlertAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUsersService usersService) : BaseController
{
    // GET: api/User
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetUsersDto>>> GetUser()
    {
        var users = await usersService.GetAllUsersAsync();
        return ToActionResult(users);
    }

    // GET: api/User/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetUserDto>> GetUser(System.Guid id)
    {
        var result = await usersService.GetUserByIdAsync(id);
        return ToActionResult(result);
    }

    // PUT: api/User/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(System.Guid id,UpdateUserDto userDto)
    {
        var result = await usersService.UpdateUserAsync(id, userDto);
        return ToActionResult(result);
    }

    // POST: api/User
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<GetUserDto>> PostUser(CreateUserDto userDto)
    {
        var result = await usersService.CreateUserAsync(userDto);
        if (!result.IsSuccess)
        {
            return MapErrorToResponse(result.Error);
        }
        return CreatedAtAction(nameof(GetUser), new { id = result.Value.Id }, result.Value);
    }

    // DELETE: api/User/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
       var result = await usersService.DeleteUserAsync(id);
        return ToActionResult(result);
    }
}
