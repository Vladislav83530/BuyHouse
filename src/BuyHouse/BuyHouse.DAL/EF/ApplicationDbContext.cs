using BuyHouse.DAL.Entities.AdvertEntities;
using BuyHouse.DAL.Entities.ApplicationUserEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuyHouse.DAL.EF
{
    /// <summary>
    /// Application Database context
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<FlatAdvert> FlatAdverts { get; set; }
        public DbSet<RoomAdvert> RoomAdverts { get; set; }
        public DbSet<HouseAdvert> HouseAdverts { get; set; }
        public DbSet<UserAvatar> UserAvatars { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public ApplicationDbContext() { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FlatAdvert>().OwnsMany(
                x => x.Photos,
                y =>
                {
                    y.WithOwner().HasForeignKey("OwnerId");
                    y.Property<int>("Id");
                    y.HasKey("Id");
                });

            builder.Entity<RoomAdvert>().OwnsMany(
               x => x.Photos,
               y =>
               {
                   y.WithOwner().HasForeignKey("OwnerId");
                   y.Property<int>("Id");
                   y.HasKey("Id");
               });

            builder.Entity<HouseAdvert>().OwnsMany(
                x => x.Photos,
                y =>
                {
                    y.WithOwner().HasForeignKey("OwnerId");
                    y.Property<int>("Id");
                    y.HasKey("Id");
                });

            builder.Entity<ApplicationUser>()
               .HasMany<FlatAdvert>(c => c.FlatAdverts)
               .WithOne(e => e.ApplicationUser)
               .HasForeignKey(c => c.UserID);

            builder.Entity<ApplicationUser>()
                .HasMany<HouseAdvert>(c => c.HouseAdverts)
                .WithOne(e => e.ApplicationUser)
                .HasForeignKey(c => c.UserID);

            builder.Entity<ApplicationUser>()
                .HasMany<RoomAdvert>(c => c.RoomAdverts)
                .WithOne(e => e.ApplicationUser)
                .HasForeignKey(c => c.UserID);

            builder.Entity<ApplicationUser>().HasData(
                new ApplicationUser { Id = "0f8fad5b-d9cb-469f-a165-70867728950e", UserName="Tom",UserSurname="Petrenko", 
                    Region="Lviv region", City="Lviv", Email="tom.petrenko@gmail.com" });
        }
    }
}
