using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kyc.Api.models;

public class Municipality
{
    public int MunicipalityId { get; set; }

    public string MunicipalityName { get; set; } = null!;

    public int DistrictId { get; set; }
    public District? District { get; set; }

    public ICollection<Ward> Wards { get; set; } = [];
}