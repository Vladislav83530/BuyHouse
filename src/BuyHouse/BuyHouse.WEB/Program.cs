using BuyHouse.BLL.Clients;
using BuyHouse.BLL.DTO;
using BuyHouse.BLL.Services;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.BLL.Services.Providers.JwtTokenProvider;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.DAL.Entities.ApplicationUserEntities;
using BuyHouse.WEB;
using BuyHouse.WEB.Clients;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddDataAnnotationsLocalization(options => {
        options.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(SharedResource));
    })
    .AddViewLocalization();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IPhotosService, PhotosService>();
builder.Services.AddScoped<ILikeAdvertService, LikeAdvertService>();

builder.Services.AddScoped<IAdvertFilterService<FlatAdvert, FlatAdvertFilter>, FlatAdvertFilterService>();
builder.Services.AddScoped<IAdvertFilterService<HouseAdvert, HouseAdvertFilter>, HouseAdvertFilterService>();
builder.Services.AddScoped<IAdvertFilterService<RoomAdvert, RoomAdvertFilter>, RoomAdvertFilterService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();

builder.Services.AddScoped<BuyHouseAPIClient>();
builder.Services.AddScoped<CurrencyConverterClient>();

builder.Services.AddScoped<IJwtTokenProvider, JwtTokenProvider>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

//For start application need to create secrets
builder.Services.AddAuthentication()
 .AddFacebook(config =>
{
    config.AppId = builder.Configuration.GetValue<string>("FacebookAppId");
    config.AppSecret = builder.Configuration.GetValue<string>("FacebookAppSecret");
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/account/google-login";
}).AddGoogle(config =>
{
    config.ClientId = builder.Configuration.GetValue<string>("GoogleClientId");
    config.ClientSecret = builder.Configuration.GetValue<string>("GoogleClientSecret");
});

//Localization
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en"),
        new CultureInfo("uk"),

    };
    options.DefaultRequestCulture = new RequestCulture("uk");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRequestLocalization();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
