using AutoMapper;
using AutoMapper.QueryableExtensions;
using FaintAlertAPI.Contracts;
using FaintAlertAPI.Data;
using FaintAlertAPI.DTOs.EmergencyContact;
using FaintAlertAPI.Results;
using Microsoft.EntityFrameworkCore;
namespace FaintAlertAPI.Services;

public class EmergencyContactsService(FaintAlertDbContext context, IMapper mapper) : IEmergencyContactsService
{
    public async Task<Result<IEnumerable<GetEmergencyContactsDto>>> GetAllEmergencyContactsAsync()
    {
        var contacts = await context.EmergencyContacts
            .ProjectTo<GetEmergencyContactsDto>(mapper.ConfigurationProvider)
            .ToListAsync();
        return Result<IEnumerable<GetEmergencyContactsDto>>.Success(contacts);
    }

    public async Task<Result<GetEmergencyContactDto>> GetEmergencyContactByIdAsync(Guid id)
    {
        var emergencyContact = await context.EmergencyContacts
            .Where(e => e.Id == id)
            .Include(e => e.User)
            .ProjectTo<GetEmergencyContactDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        return emergencyContact is null
            ? Result<GetEmergencyContactDto>.Failure(new Error("NotFoundError", "Emergency contact not found."))
            : Result<GetEmergencyContactDto>.Success(emergencyContact);
    }
    public async Task<Result> UpdateEmergencyContactAsync(Guid id, UpdateEmergencyContactDto emergencyContactDto)
    {
        var emergencyContact = await context.EmergencyContacts.FindAsync(id);
        if (emergencyContact == null)
        {
            return Result.Failure(new Error("NotFoundError", "Emergency contact not found."));
        }
        mapper.Map(emergencyContactDto, emergencyContact);
        await context.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result<GetEmergencyContactDto>> CreateEmergencyContactAsync(CreateEmergencyContactDto emergencyContactDto)
    {
        var emergencyContact = mapper.Map<EmergencyContact>(emergencyContactDto);
        context.EmergencyContacts.Add(emergencyContact);
        await context.SaveChangesAsync();
        var resultDto = await context.EmergencyContacts
            .Where(e => e.Id == emergencyContact.Id)
            .ProjectTo<GetEmergencyContactDto>(mapper.ConfigurationProvider)
            .SingleAsync();
        return Result<GetEmergencyContactDto>.Success(resultDto);
    }

    public async Task<Result> DeleteEmergencyContactAsync(Guid id)
    {
        var emergencyContact = await context.EmergencyContacts.FindAsync(id);
        if (emergencyContact == null)
        {
            return Result.Failure(new Error("NotFoundError", "Emergency contact not found."));
        }

        context.EmergencyContacts.Remove(emergencyContact);
        await context.SaveChangesAsync();
        return Result.Success();
    }
}
