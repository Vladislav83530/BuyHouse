using BuyHouse.BLL.DTO.AdvertDTO;
using BuyHouse.BLL.Services;
using BuyHouse.BLL.Services.Abstract;
using BuyHouse.DAL.EF;
using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.DAL.Repositories;
using BuyHouse.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IRepository<FlatAdvert>, FlatAdvertRepository>();
builder.Services.AddScoped<IRepository<HouseAdvert>, HouseAdvertRepository>();
builder.Services.AddScoped<IRepository<RoomAdvert>, RoomAdvertRepository>();

builder.Services.AddScoped<IAdvertService<FlatAdvertDTO, FlatAdvert>, FlatAdvertService>();
builder.Services.AddScoped<IAdvertService<HouseAdvertDTO, HouseAdvert>, HouseAdvertService>();
builder.Services.AddScoped<IAdvertService<RoomAdvertDTO, RoomAdvert>, RoomAdvertService>();

builder.Services.AddScoped<IGeneralAdvertService<FlatAdvertDTO, FlatAdvert>, FlatAdvertService>();
builder.Services.AddScoped<IGeneralAdvertService<HouseAdvertDTO, HouseAdvert>, HouseAdvertService>();
builder.Services.AddScoped<IGeneralAdvertService<RoomAdvertDTO, RoomAdvert>, RoomAdvertService>();

builder.Services.AddScoped<IPhotosService, PhotosService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
