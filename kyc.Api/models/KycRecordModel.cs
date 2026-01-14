using System.ComponentModel.DataAnnotations;

namespace kyc.Api.models;

public class KycRecordModel
{
    public int KycId { get; set; }

    public string? FullName { get; set; } = null!;

    public string? PhoneNo { get; set; }

    public string? Email { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public int? ProvinceId { get; set; }
    public int? DistrictId { get; set; }
    public int? MunicipalityId { get; set; }
    public int? WardId { get; set; }

    public DateTime CreatedDate { get; set; }

    // Navigation properties
    public Province? Province { get; set; }
    public District? District { get; set; }
    public Municipality? Municipality { get; set; }
    public Ward? Ward { get; set; }
}