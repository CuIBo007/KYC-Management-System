using kyc.Api.models;

namespace kyc.Api.Data;

public static class DbSeeder
{
    public static void Seed(KycDbContext context)
    {
        // Only seed if provinces table is empty
        if (!context.Province.Any())
        {
            // -------------------- Provinces --------------------
            var provinces = new List<Province>
            {
                new() { ProvinceName = "Province 1" },
                new() { ProvinceName = "Province 2" },
                new() { ProvinceName = "Bagmati Province" },
                new() { ProvinceName = "Gandaki Province" },
                new() { ProvinceName = "Province 5" },
                new() { ProvinceName = "Karnali Province" },
                new() { ProvinceName = "Sudurpashchim Province" }
            };
            context.Province.AddRange(provinces);
            context.SaveChanges();

            // -------------------- Districts --------------------
            var districts = new List<District>
            {
                new() { DistrictName = "Jhapa", ProvinceId = provinces[0].ProvinceId },
                new() { DistrictName = "Morang", ProvinceId = provinces[0].ProvinceId },
                new() { DistrictName = "Kathmandu", ProvinceId = provinces[2].ProvinceId },
                new() { DistrictName = "Lalitpur", ProvinceId = provinces[2].ProvinceId },
                new() { DistrictName = "Pokhara", ProvinceId = provinces[3].ProvinceId },
                new() { DistrictName = "Dang", ProvinceId = provinces[4].ProvinceId }
            };
            context.District.AddRange(districts);
            context.SaveChanges();

            // -------------------- Municipalities --------------------
            var municipalities = new List<Municipality>
            {
                new() { MunicipalityName = "Birtamod", DistrictId = districts[0].DistrictId },
                new() { MunicipalityName = "Damak", DistrictId = districts[0].DistrictId },
                new() { MunicipalityName = "Biratnagar", DistrictId = districts[1].DistrictId },
                new() { MunicipalityName = "Kathmandu Metropolitan", DistrictId = districts[2].DistrictId },
                new() { MunicipalityName = "Lalitpur Metropolitan", DistrictId = districts[3].DistrictId },
                new() { MunicipalityName = "Pokhara Metropolitan", DistrictId = districts[4].DistrictId },
                new() { MunicipalityName = "Tulsipur", DistrictId = districts[5].DistrictId }
            };
            context.Municipality.AddRange(municipalities);
            context.SaveChanges();

            // -------------------- Wards --------------------
            var wards = new List<Ward>();
            foreach (var mun in municipalities)
            {
                // Example: 1–3 wards per municipality
                for (int i = 1; i <= 3; i++)
                {
                    wards.Add(new Ward { MunicipalityId = mun.MunicipalityId, WardNo = i });
                }
            }
            context.Ward.AddRange(wards);
            context.SaveChanges();
        }
    }
}
