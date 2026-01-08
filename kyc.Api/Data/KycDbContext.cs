using Microsoft.EntityFrameworkCore;
using kyc.Api.models;

namespace kyc.Api.Data;

public class KycDbContext(DbContextOptions<KycDbContext> options) : DbContext(options)
{
    public DbSet<Province> Province { get; set; } = null!;
    public DbSet<District> District { get; set; } = null!;
    public DbSet<Municipality> Municipality { get; set; } = null!;
    public DbSet<Ward> Ward { get; set; } = null!;
    public DbSet<KycRecordModel> KycRecord { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // -------------------- Province --------------------
        modelBuilder.Entity<Province>(b =>
        {
            b.ToTable("Province");

            b.HasKey(p => p.ProvinceId);
            b.Property(p => p.ProvinceId).ValueGeneratedOnAdd();
            b.Property(p => p.ProvinceName)
                .IsRequired()
                .HasMaxLength(200);
        });

        // -------------------- District --------------------
        modelBuilder.Entity<District>(b =>
        {
            b.ToTable("District");

            b.HasKey(d => d.DistrictId);
            b.Property(d => d.DistrictId).ValueGeneratedOnAdd();
            b.Property(d => d.DistrictName)
                .IsRequired()
                .HasMaxLength(200);

            b.HasOne(d => d.Province)
                .WithMany(p => p.Districts)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // -------------------- Municipality --------------------
        modelBuilder.Entity<Municipality>(b =>
        {
            b.ToTable("Municipality");

            b.HasKey(m => m.MunicipalityId);
            b.Property(m => m.MunicipalityId).ValueGeneratedOnAdd();
            b.Property(m => m.MunicipalityName)
                .IsRequired()
                .HasMaxLength(200);

            b.HasOne(m => m.District)
                .WithMany(d => d.Municipalities)
                .HasForeignKey(m => m.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // -------------------- Ward --------------------
        modelBuilder.Entity<Ward>(b =>
        {
            b.ToTable("Ward");

            b.HasKey(w => w.WardId);
            b.Property(w => w.WardId).ValueGeneratedOnAdd();
            b.Property(w => w.WardNo)
                .IsRequired();

            b.HasIndex(w => new { w.MunicipalityId, w.WardNo })
                .IsUnique();

            b.HasOne(w => w.Municipality)
                .WithMany(m => m.Wards)
                .HasForeignKey(w => w.MunicipalityId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // -------------------- KYC Record --------------------
        modelBuilder.Entity<KycRecordModel>(b =>
        {
            b.ToTable("KycRecord");

            b.HasKey(k => k.KycId);
            b.Property(k => k.KycId).ValueGeneratedOnAdd();

            b.Property(k => k.FullName)
                .IsRequired()
                .HasMaxLength(200);

            b.Property(k => k.PhoneNo)
                .HasMaxLength(50);

            b.Property(k => k.Email)
                .HasMaxLength(200);

            b.Property(k => k.CreatedDate)
                .HasDefaultValueSql("SYSUTCDATETIME()");

            b.HasOne(k => k.Province)
                .WithMany()
                .HasForeignKey(k => k.ProvinceId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(k => k.District)
                .WithMany()
                .HasForeignKey(k => k.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(k => k.Municipality)
                .WithMany()
                .HasForeignKey(k => k.MunicipalityId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(k => k.Ward)
                .WithMany()
                .HasForeignKey(k => k.WardId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(modelBuilder);
    }
}
