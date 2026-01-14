using System.ComponentModel.DataAnnotations;

namespace Kyc.Shared.Models;

public class KycRecordDto
{
   [Key]
   public int KycId {get; set;}

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

    [Required(ErrorMessage = "Province is required")]
    public int? ProvinceId { get; set; }

    [Required(ErrorMessage = "District is required")]
    public int? DistrictId { get; set; }

    [Required(ErrorMessage = "Municipality is required")]
    public int? MunicipalityId { get; set; }

    [Required(ErrorMessage = "Ward No is required")]
    public int? WardId { get; set; }
    public DateTime CreatedDate { get; set; }
}
