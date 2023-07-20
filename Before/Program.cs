global using Before.Service.ServiceProduct;
global using Before.Service.ProductService;
global using Before.Service.ServiceAddition.ServiceCategory;
global using Before.Service.ServiceAddition.ServiceColor;
global using Before.Service.ServiceAddition.ServiceSeason;
global using Before.Service.ServiceAddition.ServiceSeller;
global using Before.Service.ServiceAddition.ServiceSize;
global using Before.Service.ServiceAddition.ServiceTypeItem;
global using Before.Service.ServiceBlazor;
global using Before.Service.ServiceFillter;
using Before.Areas.Identity;
using Before.Data;
using Before.Data.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var dbSettingsSection = builder.Configuration.GetSection("MongoDb");
var dbSettings = dbSettingsSection.Get<MongoDBSettings>();


builder.Services.AddSingleton<IMongoDBSettings>(dbSettings);
builder.Services.AddScoped(sp => new MongoDbContext(sp.GetRequiredService<IMongoDBSettings>()));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped(sp => new HttpClient());
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();
builder.Services.AddScoped<ITypeItem, TypeItemService>();
builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<ISeller, SellerService>();
builder.Services.AddScoped<IColor, ColorService>();
builder.Services.AddScoped<ISize, SizeService>();
builder.Services.AddScoped<ISeason, SeasonService>();
builder.Services.AddScoped<IFillter, FillterService>();
builder.Services.AddSingleton<BlazorService>();
builder.Services.AddSingleton<ActiveUserCount>();
builder.Services.AddTransient<CircuitHandler, ActiveUserCircuitHandler>();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
});

app.Run();
