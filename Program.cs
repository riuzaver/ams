using ams_finstek_dotnet.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<AmsContext>(cfg =>
{
    cfg.UseSqlServer();
});
builder.Services.AddScoped<IAmsRepository, AmsRepository>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(cfg =>
{
    cfg.UseSqlServer();
});
   
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Host.ConfigureAppConfiguration(AddConfiguration);

void AddConfiguration(HostBuilderContext ctx, IConfigurationBuilder bldr)
{
    bldr.Sources.Clear();
    bldr.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("config.json")
        .AddEnvironmentVariables(); 
}

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
 .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
 
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
