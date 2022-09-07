using BuyHouse.DAL.Entities;
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
        }
    }
}
