using AutoMapper;
using FaintAlertAPI.Data;
using FaintAlertAPI.DTOs.EmergencyContact;
using FaintAlertAPI.DTOs.User;

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

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, GetUserDto>();
        CreateMap<User, GetUsersDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}
