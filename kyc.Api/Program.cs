using kyc.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// API only
builder.Services.AddControllers();

builder.Services.AddDbContext<KycDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KycDatabase")));

var app = builder.Build();

// Database setup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<KycDbContext>();
    context.Database.Migrate();
    DbSeeder.Seed(context);
}

app.UseStaticFiles();
app.UseRouting();

// API endpoints ONLY
app.MapControllers();

app.Run();
