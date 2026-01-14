using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kyc.Api.Data;
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

    // Cascading GET Endpoints 

    [HttpGet("province")]
    public async Task<ActionResult<List<ProvinceDto>>> GetProvinces()
    {
        var provinces = await _context.Province
            .AsNoTracking()
            .Select(p => new ProvinceDto
            {
                ProvinceId = p.ProvinceId,
                ProvinceName = p.ProvinceName
            })
            .ToListAsync();

        return Ok(provinces);
    }

    [HttpGet("district/{provinceId}")]
    public async Task<ActionResult<List<DistrictDto>>> GetDistricts(int provinceId)
    {
        var districts = await _context.District
            .Where(d => d.ProvinceId == provinceId)
            .AsNoTracking()
            .Select(d => new DistrictDto
            {
                DistrictId = d.DistrictId,
                DistrictName = d.DistrictName,
                ProvinceId = d.ProvinceId
            })
            .ToListAsync();

        return Ok(districts);
    }

    [HttpGet("municipalities/{districtId}")]
    public async Task<ActionResult<List<MunicipalityDto>>> GetMunicipalities(int districtId)
    {
        var municipalities = await _context.Municipality
            .Where(m => m.DistrictId == districtId)
            .AsNoTracking()
            .Select(m => new MunicipalityDto
            {
                MunicipalityId = m.MunicipalityId,
                MunicipalityName = m.MunicipalityName,
                DistrictId = m.DistrictId
            })
            .ToListAsync();

        return Ok(municipalities);
    }

    [HttpGet("wards/{municipalityId}")]
    public async Task<ActionResult<List<WardDto>>> GetWards(int municipalityId)
    {
        var wards = await _context.Ward
            .Where(w => w.MunicipalityId == municipalityId)
            .AsNoTracking()
            .Select(w => new WardDto
            {
                WardId = w.WardId,
                WardNo = w.WardNo,
                MunicipalityId = w.MunicipalityId
            })
            .ToListAsync();

        return Ok(wards);
    }

    // Get All" Endpoints for Dictionaries (for table display) 

    [HttpGet("district")]
    public async Task<ActionResult<List<DistrictDto>>> GetAllDistricts()
    {
        var districts = await _context.District
            .AsNoTracking()
            .Select(d => new DistrictDto
            {
                DistrictId = d.DistrictId,
                DistrictName = d.DistrictName,
                ProvinceId = d.ProvinceId
            })
            .ToListAsync();

        return Ok(districts);
    }

    [HttpGet("municipality")]
    public async Task<ActionResult<List<MunicipalityDto>>> GetAllMunicipalities()
    {
        var municipalities = await _context.Municipality
            .AsNoTracking()
            .Select(m => new MunicipalityDto
            {
                MunicipalityId = m.MunicipalityId,
                MunicipalityName = m.MunicipalityName,
                DistrictId = m.DistrictId
            })
            .ToListAsync();

        return Ok(municipalities);
    }

    [HttpGet("ward")]
    public async Task<ActionResult<List<WardDto>>> GetAllWards()
    {
        var wards = await _context.Ward
            .AsNoTracking()
            .Select(w => new WardDto
            {
                WardId = w.WardId,
                WardNo = w.WardNo,
                MunicipalityId = w.MunicipalityId
            })
            .ToListAsync();

        return Ok(wards);
    }
}
