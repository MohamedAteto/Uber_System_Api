using Microsoft.EntityFrameworkCore;
using Uber_System_Api.Models;

namespace Uber_System_Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Ride> Rides { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Rating> Rating { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


         

            modelBuilder.Entity<User>()
                .HasOne(u => u.Vehicle)
                .WithOne(v => v.Driver)
                .HasForeignKey<Vehicle>(v => v.DriverId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<User>()
                .HasMany(u => u.Rides)
                .WithOne(r => r.UserRider)
                .HasForeignKey(r => r.RiderId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<User>()
                .HasMany(r => r.DriverRides)
                .WithOne(m => m.UserDriver)
                .HasForeignKey(m => m.DriverId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Ride>()
                .HasOne(r => r.Payment)
                .WithOne(p => p.Ride)
                .HasForeignKey<Payment>(p => p.RideId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ride>()
                .HasOne(r => r.rating)
                .WithOne(p => p.Ride)
                .HasForeignKey<Rating>(p => p.RideId)
                .OnDelete(DeleteBehavior.NoAction);



            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.UserFullName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.Password).IsRequired();
                entity.Property(u => u.PhoneNumber).IsRequired();
                entity.Property(u => u.Role).IsRequired();
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(v => v.Id);
                entity.Property(v => v.VehiclePlateNumber).IsRequired().HasMaxLength(20);
                entity.Property(v => v.VehicleColor).IsRequired();
            });

            modelBuilder.Entity<Ride>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.PickupLocation).IsRequired().HasMaxLength(150);
                entity.Property(r => r.DropOffLocation).IsRequired().HasMaxLength(150);
                entity.Property(r => r.Fare).HasColumnType("decimal(18,2)");
                entity.Property(r => r.Status).IsRequired();

                entity.ToTable(tb => tb.HasCheckConstraint("CK_Ride_Fare", "[Fare] >= 20 AND [Fare] <= 5000"));
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(rt => rt.Id);
                entity.Property(rt => rt.Score).HasColumnType("decimal(18,2)");

                entity.ToTable(tb => tb.HasCheckConstraint("CK_Rating_Score", "[Score] >= 1 AND [Score] <= 5"));
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Amount).HasColumnType("decimal(18,2)");
                entity.Property(p => p.PaymentMethod).IsRequired();
                entity.Property(p => p.PaymentDate).IsRequired();
                entity.Property(p => p.Status).IsRequired();

                entity.ToTable(tb => tb.HasCheckConstraint("CK_Payment_Amount", "[Amount] >= 20 AND [Amount] <= 5000"));
            });



            // Data Seeding

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserFullName = "John Doe",
                    Email = "john.driver@uber.com",
                    Password = "SecurePassword123", 
                    PhoneNumber = "01234567890",
                    Role = "Driver"
                },
                new User
                {
                    Id = 2,
                    UserFullName = "Jane Smith",
                    Email = "jane.rider@gmail.com",
                    Password = "SecurePassword123",
                    PhoneNumber = "00987654321",
                    Role = "Rider"
                }
            );

            
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle
                {
                    Id = 1,
                    VehiclePlateNumber = "ABC-1234",
                    VehicleColor = "Black",
                    DriverId = 1
                }
            );

            
            modelBuilder.Entity<Ride>().HasData(
                new Ride
                {
                    Id = 1,
                    PickupLocation = "Downtown Station",
                    DropOffLocation = "International Airport",
                    Fare = 150,
                    Status = "Completed",
                    RiderId = 2,
                    DriverId = 1
                }
            );

            
            modelBuilder.Entity<Payment>().HasData(
                new Payment
                {
                    Id = 1,
                    Amount = 150,
                    PaymentMethod = "Credit Card",
                    PaymentDate = new DateTime(2026, 6, 1),
                    Status = "Success",
                    RideId = 1
                }
            );

            modelBuilder.Entity<Rating>().HasData(
                new Rating
                {
                    Id = 1,
                    Score = 5,
                    RideId = 1,
                    
                }
            );
        }

    }
}
