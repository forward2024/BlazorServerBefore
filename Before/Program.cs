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
global using Microsoft.EntityFrameworkCore;
using Before.Areas.Identity;
using Before.Data;
using Before.Data.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Identity;
using Before.Service.ServiceLikeCart;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;



var builder = WebApplication.CreateBuilder(args);



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

var dbSettingsSection = builder.Configuration.GetSection("MongoDb");
var dbSettings = dbSettingsSection.Get<MongoDBSettings>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
    .AddCookie()
.AddGoogle(options =>
{
    options.ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID") ?? throw new ArgumentNullException("GOOGLE_CLIENT_ID");
    options.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET") ?? throw new ArgumentNullException("GOOGLE_CLIENT_SECRET");
});




builder.Services.AddSingleton<IMongoDBSettings>(dbSettings);
builder.Services.AddScoped(sp => new MongoDbContext(sp.GetRequiredService<IMongoDBSettings>()));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(1);
});
builder.Services.AddScoped(sp => new HttpClient());
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<User>>();
builder.Services.AddScoped<ITypeItem, TypeItemService>();
builder.Services.AddScoped<IProduct, ProductService>();
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<ISeller, SellerService>();
builder.Services.AddScoped<IColor, ColorService>();
builder.Services.AddScoped<ISize, SizeService>();
builder.Services.AddScoped<ISeason, SeasonService>();
builder.Services.AddScoped<IFilter, FilterService>();
builder.Services.AddScoped<ILikeCartService, LikeCartService>();
builder.Services.AddSingleton<BlazorService>();
builder.Services.AddSingleton<ActiveUserCount>();
builder.Services.AddTransient<CircuitHandler, ActiveUserCircuitHandler>();



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
    var typeColorService = scope.ServiceProvider.GetRequiredService<IColor>();
    var typeSizeService = scope.ServiceProvider.GetRequiredService<ISize>();
    var typeSeasonService = scope.ServiceProvider.GetRequiredService<ISeason>();
    await typeItemService.GetAllAsync();
    await productService.GetAllAsync();
    await categoryService.GetAllAsync();
    await sellerService.GetAllAsync();
    await typeColorService.GetAllAsync();
    await typeSizeService.GetAllAsync();
    await typeSeasonService.GetAllAsync();
    scope.ServiceProvider.GetRequiredService<BlazorService>();
}


if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSession();
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
