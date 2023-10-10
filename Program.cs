using Microsoft.EntityFrameworkCore;
using NBPExchangeRates;
using NBPExchangeRates.Interfaces;
using NBPExchangeRates.Models;
using NBPExchangeRates.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<NBPContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NBPContext")));
builder.Services.AddOptions<NbpApiOptions>().BindConfiguration("NbpApiOptions");
builder.Services.AddScoped<IDatabaseCommunicationService, DatabaseCommunicationService>();
builder.Services.AddScoped<INBPAPICommunicationService, NBPAPICommunicationService>();
builder.Services.AddScoped<IDataProviderService, DataProviderService>();
builder.Services.AddHttpClient("NBPAPIClient", client =>
{
    client.Timeout = new TimeSpan(0, 0, 30);
    client.DefaultRequestHeaders.Clear();
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
