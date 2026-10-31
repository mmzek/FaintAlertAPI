using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FaintAlertAPI.Data;
using FaintAlertAPI.DTOs.EmergencyContact;

[Route("api/[controller]")]
[ApiController]
public class EmergencyContactsController(FaintAlertDbContext context) : ControllerBase
{

    // GET: api/EmergencyContact
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetEmergencyContactsDto>>> GetEmergencyContact()
    {
        var emergencyContacts = await context.EmergencyContacts
            .Select(e => new GetEmergencyContactsDto(
                e.Id,
                e.Name,
                e.Email,
                e.Phone,
                e.UserId
            ))
            .ToListAsync();
        return Ok(emergencyContacts);
    }

    // GET: api/EmergencyContact/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetEmergencyContactDto>> GetEmergencyContact(System.Guid id)
    {
        var emergencycontact = await context.EmergencyContacts
            .Where(e => e.Id == id)
            .Select(e => new GetEmergencyContactDto(
                e.Id,
                e.Name,
                e.Email,
                e.Phone,
                e.User!.Name
            ))
            .FirstOrDefaultAsync();

        if (emergencycontact == null)
        {
            return NotFound();
        }

        return emergencycontact;
    }

    // PUT: api/EmergencyContact/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmergencyContact(System.Guid? id, UpdateEmergencyContactDto emergencycontactDto)
    {
        if (id != emergencycontactDto.Id)
        {
            return BadRequest();
        }

        var emergency = await context.EmergencyContacts.FindAsync(id);
        if (emergency == null)
        {
            return NotFound();
        }

        emergency.Name = emergencycontactDto.Name;
        emergency.Email = emergencycontactDto.Email;
        emergency.Phone = emergencycontactDto.Phone;

        context.Entry(emergency).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await EmergencyContactExists(id))
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

    // POST: api/EmergencyContact
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<EmergencyContact>> PostEmergencyContact(CreateEmergencyContactDto emergencycontactDto)
    {
        var emergencycontact = new EmergencyContact
        {
            Id = Guid.NewGuid(),
            Name = emergencycontactDto.Name,
            Email = emergencycontactDto.Email,
            Phone = emergencycontactDto.Phone,
            UserId = emergencycontactDto.UserId
        };

        context.EmergencyContacts.Add(emergencycontact);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetEmergencyContact", new { id = emergencycontact.Id }, emergencycontact);
    }

    // DELETE: api/EmergencyContact/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmergencyContact(System.Guid? id)
    {
        var emergencycontact = await context.EmergencyContacts.FindAsync(id);
        if (emergencycontact == null)
        {
            return NotFound();
        }

        context.EmergencyContacts.Remove(emergencycontact);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> EmergencyContactExists(System.Guid? id)
    {
        return await context.EmergencyContacts.AnyAsync(e => e.Id == id);
    }
}
