using System.ComponentModel.DataAnnotations;

namespace FaintAlertAPI.DTOs.Me;

public sealed record UpdateMeDto(
    [property: Required, MaxLength(100)] string Name);
