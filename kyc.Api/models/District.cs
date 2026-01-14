using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kyc.Api.models;

public class District
{
    public int DistrictId { get; set; }

    public string DistrictName { get; set; } = null!;

    public int ProvinceId { get; set; }
    public Province? Province { get; set; }

    public ICollection<Municipality> Municipalities { get; set; } = [];
}