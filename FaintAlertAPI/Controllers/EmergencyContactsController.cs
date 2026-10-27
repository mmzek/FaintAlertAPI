using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FaintAlertAPI.Data;

[Route("api/[controller]")]
[ApiController]
public class EmergencyContactsController(FaintAlertDbContext context) : ControllerBase
{

    // GET: api/EmergencyContact
    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmergencyContact>>> GetEmergencyContact()
    {
        return await context.EmergencyContacts.ToListAsync();
    }

    // GET: api/EmergencyContact/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EmergencyContact>> GetEmergencyContact(System.Guid id)
    {
        var emergencycontact = await context.EmergencyContacts
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (emergencycontact == null)
        {
            return NotFound();
        }

        return emergencycontact;
    }

    // PUT: api/EmergencyContact/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmergencyContact(System.Guid? id, EmergencyContact emergencycontact)
    {
        if (id != emergencycontact.Id)
        {
            return BadRequest();
        }

        context.Entry(emergencycontact).State = EntityState.Modified;

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
    public async Task<ActionResult<EmergencyContact>> PostEmergencyContact(EmergencyContact emergencycontact)
    {
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
