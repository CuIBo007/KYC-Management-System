using System.ComponentModel.DataAnnotations;

namespace kyc.Api.models;

public class Ward
{
    public int WardId { get; set; }

    public int WardNo { get; set; }

    public int MunicipalityId { get; set; }
    public Municipality? Municipality { get; set; }
}