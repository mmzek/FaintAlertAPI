using FaintAlertAPI.DTOs.EmergencyContact;
using FaintAlertAPI.Results;

namespace FaintAlertAPI.Contracts
{
    public interface IEmergencyContactsService
    {
        Task<Result<GetEmergencyContactDto>> CreateEmergencyContactAsync(CreateEmergencyContactDto emergencyContactDto);
        Task<Result> DeleteEmergencyContactAsync(Guid id);
        Task<Result<IEnumerable<GetEmergencyContactsDto>>> GetAllEmergencyContactsAsync();
        Task<Result<GetEmergencyContactDto>> GetEmergencyContactByIdAsync(Guid id);
        Task<Result> UpdateEmergencyContactAsync(Guid id, UpdateEmergencyContactDto emergencycontactDto);
    }
}