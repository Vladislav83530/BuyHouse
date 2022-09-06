using BuyHouse.DAL.Entities;
using BuyHouse.DAL.Entities.ApplicationUserEntity;
using BuyHouse.DAL.Entities.FlatEntity;
using BuyHouse.DAL.Entities.RoomEntity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuyHouse.DAL.EF
{
    /// <summary>
    /// Application Database context
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Flat> Flats { get; set; }
        public DbSet<RealtyMainInfo> FlatMainInfo { get; set; }
        public DbSet<FlatParameters> FlatParameters { get; set; }
        public DbSet<RealtyPhotos> FlatPhotos { get; set; }
        public DbSet<Advert> Adverts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public ApplicationDbContext() { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Flat>()
                .HasOne<RealtyMainInfo>(x=>x.MainInfo)
                .WithOne(y=>y.Flat)
                .HasForeignKey<Flat>(u => u.FlatID);

            //builder.Entity<Room>()
            //    .HasOne<RealtyMainInfo>(x=>x.MainInfo)
            //    .WithOne(y => y.Room)
            //    .HasForeignKey<Room>(u => u.RoomID);

            builder.Entity<Flat>()
                .HasMany<RealtyPhotos>(x => x.Photos)
                .WithOne(y => y.Flat)
                .HasForeignKey(u => u.FlatID);

            //builder.Entity<Room>()
            //    .HasMany<RealtyPhotos>(x => x.Photos)
            //    .WithOne(y => y.Room)
            //    .HasForeignKey(u => u.RoomID);

            builder.Entity<Flat>()
                .HasOne<FlatParameters>(x => x.Parameters)
                .WithOne(y => y.Flat)
                .HasForeignKey<Flat>(u => u.FlatID);

            builder.Entity<Room>()
                .HasOne<RoomParameters>(x => x.Parameters)
                .WithOne(y => y.Room)
                .HasForeignKey<Room>(u => u.RoomID);

            builder.Entity<Advert>()
                .HasOne<Flat>(x => x.Flat)
                .WithOne(y => y.Advert)
                .HasForeignKey<Advert>(u => u.AdvertID);

            builder.Entity<ApplicationUser>()
                .HasMany<Advert>(x=>x.Adverts)
                .WithOne(y=>y.ApplicationUser)
                .HasForeignKey(u=>u.UserID);
        }
    }
}
