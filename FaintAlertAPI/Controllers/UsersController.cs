using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FaintAlertAPI.Data;
using FaintAlertAPI.DTOs.User;

[Route("api/[controller]")]
[ApiController]
public class UsersController(FaintAlertDbContext context) : ControllerBase
{

    // GET: api/User
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetUsersDto>>> GetUser()
    {
        var users = await context.Users
            .Select(u => new GetUsersDto(
                u.Id, 
                u.Name, 
                u.Email))
            .ToListAsync();
        return Ok(users);
    }

    // GET: api/User/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetUserDto>> GetUser(System.Guid id)
    {
        var user = await context.Users
            .Where(u => u.Id == id)
            .Select(u => new GetUsersDto(u.Id, u.Name, u.Email))
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    // PUT: api/User/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(System.Guid? id,UpdateUserDto userDto)
    {
        if (id != userDto.Id)
        {
            return BadRequest();
        }

        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        user.Name = userDto.Name;
        user.Email = userDto.Email;
        user.Password = userDto.Password;

        context.Entry(user).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await UserExistsAsync(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/User
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(CreateUserDto userDto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = userDto.Name,
            Email = userDto.Email,
            Password = userDto.Password
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new { id = user.Id }, user);
    }

    // DELETE: api/User/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(System.Guid? id)
    {
        var user = await context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        context.Users.Remove(user);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> UserExistsAsync(System.Guid? id)
    {
        return await context.Users.AnyAsync(e => e.Id == id);
    }
}
