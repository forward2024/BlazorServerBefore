global using Before.Service.ServiceProduct;
using Before.Areas.Identity;
using Before.Data;
using Before.Data.Models;
using Before.Service.ProductService;
using Before.Service.ServiceAddition.ServiceCategory;
using Before.Service.ServiceAddition.ServiceColor;
using Before.Service.ServiceAddition.ServiceImage;
using Before.Service.ServiceAddition.ServiceSeason;
using Before.Service.ServiceAddition.ServiceSeller;
using Before.Service.ServiceAddition.ServiceSize;
using Before.Service.ServiceAddition.ServiceTypeItem;
using Before.Service.ServiceBlazor;
using Before.Service.ServiceFillter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();



builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();

builder.Services.AddScoped<ITypeItem, TypeItemService>();
builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<ISeller, SellerService>();
builder.Services.AddScoped<IImage, ImageService>();
builder.Services.AddScoped<IColor, ColorService>();
builder.Services.AddScoped<ISize, SizeService>();
builder.Services.AddScoped<ISeason, SeasonService>();
builder.Services.AddScoped<IFillter, FillterService>();
builder.Services.AddSingleton<BlazorService>();


builder.Services.AddHttpClient("ServerHttpClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseAddress"]);
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleInitializer.InitializeAsync(userManager, roleManager);

    var typeItemService = scope.ServiceProvider.GetRequiredService<ITypeItem>();
    var productService = scope.ServiceProvider.GetRequiredService<IProduct>();
    var categoryService = scope.ServiceProvider.GetRequiredService<ICategory>();
    var sellerService = scope.ServiceProvider.GetRequiredService<ISeller>();
    var typeImageService = scope.ServiceProvider.GetRequiredService<IImage>();
    var typeColorService = scope.ServiceProvider.GetRequiredService<IColor>();
    var typeSizeService = scope.ServiceProvider.GetRequiredService<ISize>();
    var typeSeasonService = scope.ServiceProvider.GetRequiredService<ISeason>();
    await typeItemService.GetAllAsync();
    await productService.GetAllAsync();
    await categoryService.GetAllAsync();
    await sellerService.GetAllAsync();
    await typeImageService.GetAllAsync();
    await typeColorService.GetAllAsync();
    await typeSizeService.GetAllAsync();
    await typeSeasonService.GetAllAsync();
    scope.ServiceProvider.GetRequiredService<BlazorService>();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
