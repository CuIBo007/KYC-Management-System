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

    [HttpPost]
    public async Task<IActionResult> SaveKyc([FromBody] KycFormModel form)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value!.Errors.Count > 0)
                .Select(x => new
                {
                    Field = x.Key,
                    Errors = x.Value!.Errors.Select(e => e.ErrorMessage)
                });

            return BadRequest(errors);
        }

        if (form.ProvinceId == null ||
            form.DistrictId == null ||
            form.MunicipalityId == null ||
            form.WardId == null)
        {
            return BadRequest("Complete address hierarchy is required.");
        }

        var model = new KycRecordModel
        {
            FullName = form.FullName!,
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

        var result = new KycRecordDto
        {
            KycId = model.KycId,
            FullName = model.FullName,
            PhoneNo = model.PhoneNo,
            Email = model.Email,
            DateOfBirth = model.DateOfBirth,
            ProvinceId = model.ProvinceId,
            DistrictId = model.DistrictId,
            MunicipalityId = model.MunicipalityId,
            WardId = model.WardId,
            CreatedDate = model.CreatedDate
        };

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetKycList()
    {
        var list = await _context.KycRecord
            .AsNoTracking()
            .Select(k => new KycRecordDto
            {
                KycId = k.KycId,
                FullName = k.FullName,
                PhoneNo = k.PhoneNo,
                Email = k.Email,
                DateOfBirth = k.DateOfBirth,
                ProvinceId = k.ProvinceId,
                DistrictId = k.DistrictId,
                MunicipalityId = k.MunicipalityId,
                WardId = k.WardId,
                CreatedDate = k.CreatedDate
            })
            .ToListAsync();

        return Ok(list);
    }
}
