using FaintAlertAPI.Data;
using FaintAlertAPI.DTOs.Me;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FaintAlertAPI.Controllers;

[ApiController]
[Route("api/me")]
[Authorize]
public class MeController(UserManager<User> userManager) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<MeDto>> Get(CancellationToken cancellationToken)
    {
        var user = await userManager.GetUserAsync(User);

        if (user is null)
        {
            return Unauthorized();
        }

        return Ok(new MeDto(user.Id, user.Email, user.Name));
    }

    [HttpPut]
    public async Task<IActionResult> Update(
        UpdateMeDto dto,
        CancellationToken cancellationToken)
    {
        var user = await userManager.GetUserAsync(User);

        if (user is null)
        {
            return Unauthorized();
        }

        user.Name = dto.Name.Trim();
        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(new
            {
                Errors = result.Errors.Select(error => new
                {
                    error.Code,
                    error.Description
                })
            });
        }

        return NoContent();
    }
}
