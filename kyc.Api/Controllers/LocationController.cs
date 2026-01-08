using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kyc.Api.Data;
using kyc.Api.models;
using Kyc.Shared.Models;

namespace kyc.Api.Controllers;

[ApiController]
[Route("api/location")]
public class LocationController : ControllerBase
{
    private readonly KycDbContext _context;

    public LocationController(KycDbContext context)
    {
        _context = context;
    }

    // ===== Cascading GET Endpoints =====

    [HttpGet("province")]
    public async Task<IActionResult> GetProvince()
    {
        var provinces = await _context.Province.AsNoTracking().ToListAsync();
        return Ok(provinces);
    }

    [HttpGet("district/{provinceId}")]
    public async Task<IActionResult> GetDistrict(int provinceId)
    {
        var districts = await _context.District
            .Where(d => d.ProvinceId == provinceId)
            .AsNoTracking()
            .ToListAsync();
        return Ok(districts);
    }

    [HttpGet("municipalities/{districtId}")]
    public async Task<IActionResult> GetMunicipalities(int districtId)
    {
        var municipalities = await _context.Municipality
            .Where(m => m.DistrictId == districtId)
            .AsNoTracking()
            .ToListAsync();
        return Ok(municipalities);
    }

    [HttpGet("wards/{municipalityId}")]
    public async Task<IActionResult> GetWards(int municipalityId)
    {
        var wards = await _context.Ward
            .Where(w => w.MunicipalityId == municipalityId)
            .AsNoTracking()
            .ToListAsync();
        return Ok(wards);
    }

    // ===== "Get All" Endpoints for Dictionaries =====

    [HttpGet("district")]
    public async Task<IActionResult> GetAllDistricts()
    {
        var allDistricts = await _context.District.AsNoTracking().ToListAsync();
        return Ok(allDistricts);
    }

    [HttpGet("municipality")]
    public async Task<IActionResult> GetAllMunicipalities()
    {
        var allMunicipalities = await _context.Municipality.AsNoTracking().ToListAsync();
        return Ok(allMunicipalities);
    }

    [HttpGet("ward")]
    public async Task<IActionResult> GetAllWards()
    {
        var allWards = await _context.Ward.AsNoTracking().ToListAsync();
        return Ok(allWards);
    }
}
