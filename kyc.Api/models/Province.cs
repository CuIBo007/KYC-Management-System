using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace kyc.Api.models;

public class Province
{
    public int ProvinceId { get; set; }

    public string ProvinceName { get; set; } = null!;

    public ICollection<District> Districts { get; set; } = [];
}