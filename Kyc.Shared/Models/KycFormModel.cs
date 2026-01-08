namespace Kyc.Shared.Models;

public class KycFormModel
{
    public string FullName { get; set; } = string.Empty;
    public string? PhoneNo { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public int? ProvinceId { get; set; }
    public int? DistrictId { get; set; }
    public int? MunicipalityId { get; set; }
    public int? WardId { get; set; }
}
