
using Microsoft.EntityFrameworkCore;

namespace kyc.Api.Data;

public static class DataExtensions
{
   public static void MigrateDB(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<KycDbContext>();
        context.Database.Migrate();
    }
}
