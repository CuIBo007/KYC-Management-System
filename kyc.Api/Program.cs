using Microsoft.EntityFrameworkCore;
using kyc.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// 1. Register services BEFORE building the app
// --------------------

// Add controllers for API
builder.Services.AddControllers();

// Add Razor Pages and Server-Side Blazor
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add DbContext for KYC
builder.Services.AddDbContext<KycDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KycDatabase")));

// HttpClient for Blazor API calls
builder.Services.AddHttpClient("KycApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7094/"); // adjust port if needed
});

// --------------------
// 2. Build the app
// --------------------
var app = builder.Build();

// --------------------
// 3. Seed database and run migrations
// --------------------
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<KycDbContext>();

    // Apply EF Migrations (if using)
    context.Database.Migrate();

    // Seed database with Provinces/Districts/Municipalities/Wards
    DbSeeder.Seed(context);
}

// --------------------
// 4. Configure middleware
// --------------------
app.UseStaticFiles();
app.UseRouting();

// Map API controllers
app.MapControllers();

// Map Blazor hub and fallback page
app.MapBlazorHub();

// --------------------
// 5. Run the application
// --------------------
app.Run();
