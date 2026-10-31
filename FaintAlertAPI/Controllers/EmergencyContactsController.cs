using Microsoft.AspNetCore.Mvc;
using FaintAlertAPI.DTOs.EmergencyContact;
using FaintAlertAPI.Contracts;
using FaintAlertAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmergencyContactsController(IEmergencyContactsService emergencyContactsService) : BaseController
{

    // GET: api/EmergencyContact
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetEmergencyContactsDto>>> GetEmergencyContact()
    {
        var result = await emergencyContactsService.GetAllEmergencyContactsAsync();
        return ToActionResult(result);
    }

    // GET: api/EmergencyContact/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetEmergencyContactDto>> GetEmergencyContact(System.Guid id)
    {
        var result = await emergencyContactsService.GetEmergencyContactByIdAsync(id);
        return ToActionResult(result);
    }

    // PUT: api/EmergencyContact/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmergencyContact(Guid id, UpdateEmergencyContactDto emergencycontactDto)
    {
        if (id != emergencycontactDto.Id)
        {
            return BadRequest();
        }

        var result = await emergencyContactsService.UpdateEmergencyContactAsync(id, emergencycontactDto);
        return ToActionResult(result);
    }

    // POST: api/EmergencyContact
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<GetEmergencyContactDto>> PostEmergencyContact(CreateEmergencyContactDto emergencyContactDto)
    {
        var result = await emergencyContactsService.CreateEmergencyContactAsync(emergencyContactDto);
        if (!result.IsSuccess)
        {
            return MapErrorToResponse(result.Error);
        }

        return CreatedAtAction(nameof(GetEmergencyContact), new { id = result.Value.Id }, result.Value);
    }

    // DELETE: api/EmergencyContact/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmergencyContact(Guid id)
    {
        var result = await emergencyContactsService.DeleteEmergencyContactAsync(id);
        return ToActionResult(result);
    }
}
