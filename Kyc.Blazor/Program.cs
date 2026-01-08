using Kyc.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("KycApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["KycApiBaseUrl"] ?? "http://localhost:5004");
});

var app = builder.Build();

// Configure exception handling
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Middleware order is important
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

// Map Blazor endpoints
// MapRazorComponents with AddInteractiveServerRenderMode() automatically includes MapBlazorHub()
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
