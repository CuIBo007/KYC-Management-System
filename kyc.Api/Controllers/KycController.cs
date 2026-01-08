using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kyc.Api.Data;
using kyc.Api.models;
using Kyc.Shared.Models;

namespace kyc.Api.Controllers;

[ApiController]
[Route("api/kyc")]
public class KycController : ControllerBase
{
    private readonly KycDbContext _context;

    public KycController(KycDbContext context)
    {
        _context = context;
    }

    // ✅ Updated POST endpoint
    [HttpPost]
    public async Task<IActionResult> SaveKyc([FromBody] KycFormModel form)
    {
        if (form == null) return BadRequest();

        var model = new KycRecordModel
        {
            FullName = form.FullName,
            PhoneNo = form.PhoneNo,
            Email = form.Email,
            DateOfBirth = form.DateOfBirth,
            ProvinceId = form.ProvinceId,
            DistrictId = form.DistrictId,
            MunicipalityId = form.MunicipalityId,
            WardId = form.WardId,
            CreatedDate = DateTime.UtcNow
        };

        _context.KycRecord.Add(model);
        await _context.SaveChangesAsync();

        return Ok(model);
    }

    [HttpGet]
    public async Task<IActionResult> GetKycList()
    {
        var list = await _context.KycRecord
            .Include(k => k.Province)
            .Include(k => k.District)
            .Include(k => k.Municipality)
            .Include(k => k.Ward)
            .AsNoTracking()
            .ToListAsync();

        return Ok(list);
    }
}
