using System.ComponentModel.DataAnnotations;

namespace Kyc.Shared.Models;

public class KycFormModel
{
    [Required(ErrorMessage = "FullName is required")]
    [MaxLength(200)]
    public string? FullName { get; set; }

    [Required(ErrorMessage = "PhoneNo is required")]
    [MaxLength(50)]
    public string? PhoneNo { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email format")]
    [MaxLength(200)]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Date of Birth is required")]
    public DateTime? DateOfBirth { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Province is required")]
    public int ProvinceId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "District is required")]
    public int DistrictId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Municipality is required")]
    public int MunicipalityId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Ward No is required")]
    public int WardId { get; set; }
}
