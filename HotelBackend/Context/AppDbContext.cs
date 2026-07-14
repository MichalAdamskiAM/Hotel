using Hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Privilege> Privileges { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<RolePrivilege> RolePrivileges { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<RoomReservation> RoomReservations { get; set; } = null!;
        public DbSet<Amenity> Amenities { get; set; } = null!;
        public DbSet<RoomAmenity> RoomAmenities { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RolePrivilege>()
                .HasKey(rp => new { rp.RoleId, rp.PrivilegeId });

            modelBuilder.Entity<RolePrivilege>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePrivileges)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RolePrivilege>()
                .HasOne(rp => rp.Privilege)
                .WithMany(p => p.RolePrivileges)
                .HasForeignKey(rp => rp.PrivilegeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Status)
                .WithMany(s => s.Reservations)
                .HasForeignKey(r => r.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>()
                .Property(r => r.Area)
                .HasPrecision(6, 2);

            modelBuilder.Entity<Room>()
                .Property(r => r.Price)
                .HasPrecision(6, 2);

            modelBuilder.Entity<Room>()
                .HasIndex(r => r.Number)
                .IsUnique();

            modelBuilder.Entity<RoomReservation>()
                .HasKey(rr => new { rr.RoomId, rr.ReservationId });

            modelBuilder.Entity<RoomReservation>()
                .HasOne(ra => ra.Room)
                .WithMany(r => r.RoomReservations)
                .HasForeignKey(ra => ra.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoomReservation>()
                .HasOne(ra => ra.Reservation)
                .WithMany(r => r.RoomReservations)
                .HasForeignKey(ra => ra.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RoomAmenity>()
                .HasKey(ra => new { ra.RoomId, ra.AmenityId });

            modelBuilder.Entity<RoomAmenity>()
                .HasOne(ra => ra.Room)
                .WithMany(r => r.RoomAmenities)
                .HasForeignKey(ra => ra.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RoomAmenity>()
                .HasOne(ra => ra.Amenity)
                .WithMany(r => r.RoomAmenities)
                .HasForeignKey(ra => ra.AmenityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Privilege>().HasData(
                new { Id = 1, Name = "SeeOwnReservations" },
                new { Id = 2, Name = "SeeAllRooms" },
                new { Id = 3, Name = "SeeAllUsers" },
                new { Id = 4, Name = "SeeAllReservations" },
                new { Id = 5, Name = "ManageOwnReservationsDates" },
                new { Id = 6, Name = "ManageOwnReservationsRooms" },
                new { Id = 7, Name = "ManageAllReservationsDates" },
                new { Id = 8, Name = "ManageAllReservationsRooms" },
                new { Id = 9, Name = "ManageAllReservationsStatus" },
                new { Id = 10, Name = "ManageAllRooms" },
                new { Id = 11, Name = "ManageAllUsers" }
            );

            modelBuilder.Entity<Role>().HasData(
                new { Id = 1, Name = "Customer" },
                new { Id = 2, Name = "Receptionist" },
                new { Id = 3, Name = "Administrator" }
            );

            modelBuilder.Entity<RolePrivilege>().HasData(
                new { RoleId = 1, PrivilegeId = 1 },
                new { RoleId = 1, PrivilegeId = 2 },
                new { RoleId = 1, PrivilegeId = 5 },
                new { RoleId = 1, PrivilegeId = 6 },

                new { RoleId = 2, PrivilegeId = 2 },
                new { RoleId = 2, PrivilegeId = 3 },
                new { RoleId = 2, PrivilegeId = 4 },
                new { RoleId = 2, PrivilegeId = 7 },
                new { RoleId = 2, PrivilegeId = 8 },
                new { RoleId = 2, PrivilegeId = 9 },

                new { RoleId = 3, PrivilegeId = 2 },
                new { RoleId = 3, PrivilegeId = 3 },
                new { RoleId = 3, PrivilegeId = 4 },
                new { RoleId = 3, PrivilegeId = 7 },
                new { RoleId = 3, PrivilegeId = 8 },
                new { RoleId = 3, PrivilegeId = 9 },
                new { RoleId = 3, PrivilegeId = 10 },
                new { RoleId = 3, PrivilegeId = 11 }
            );

            modelBuilder.Entity<User>().HasData(
                // Default Admin
                new User
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@gmail.com",
                    Phone = "123456789",
                    Password = "$2a$12$5Tu.tWBGVpyJTAq11HjdHeYowvZzvi/izmVX1NS3ISGHdC/Q/dJJG", //zaq1@WSX
                    RoleId = 3
                }
            );

            modelBuilder.Entity<Status>().HasData(
                new { Id = 1, Name = "Pending" },
                new { Id = 2, Name = "Confirmed" },
                new { Id = 3, Name = "Paid" },
                new { Id = 4, Name = "Started" },
                new { Id = 5, Name = "Completed" },
                new { Id = 6, Name = "Cancelled" }
            );

            modelBuilder.Entity<Amenity>().HasData(
                new { Id = 1, Name = "Balcony" },
                new { Id = 2, Name = "Sea view" },
                new { Id = 3, Name = "Refrigerator" },
                new { Id = 4, Name = "Kettle" },
                new { Id = 5, Name = "Air conditioning" },
                new { Id = 6, Name = "Safe" },
                new { Id = 7, Name = "TV" }
            );
        }
    }
}