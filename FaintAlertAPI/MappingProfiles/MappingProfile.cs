using AutoMapper;
using FaintAlertAPI.Data;
using FaintAlertAPI.DTOs.EmergencyContact;

namespace FaintAlertAPI.MappingProfiles;

public class EmergencyContactMappingProfile : Profile
{
    public EmergencyContactMappingProfile()
    {
        CreateMap<EmergencyContact, GetEmergencyContactDto>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(source => source.User == null ? string.Empty : source.User.Name));
        CreateMap<EmergencyContact, GetEmergencyContactsDto>();
        CreateMap<CreateEmergencyContactDto, EmergencyContact>();
        CreateMap<UpdateEmergencyContactDto, EmergencyContact>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
