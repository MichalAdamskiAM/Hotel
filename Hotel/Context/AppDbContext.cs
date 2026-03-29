using Microsoft.EntityFrameworkCore;
using Hotel.Models;

namespace Hotel.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Privilege> Privileges { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<RolePrivilege> RolePrivileges { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<Room> Rooms { get; set; } = null!;
        public DbSet<RoomReservation> RoomReservations { get; set; } = null!;

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
                .HasKey(r => r.Number);

            modelBuilder.Entity<Room>()
                .Property(r => r.Area)
                .HasPrecision(6, 2);

            modelBuilder.Entity<RoomReservation>()
                .HasKey(rr => new { rr.RoomNumber, rr.ReservationId });

            modelBuilder.Entity<RoomReservation>()
                .HasOne(rr => rr.Room)
                .WithMany(r => r.RoomReservations)
                .HasForeignKey(rr => rr.RoomNumber)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoomReservation>()
                .HasOne(rr => rr.Reservation)
                .WithMany(r => r.RoomReservations)
                .HasForeignKey(rr => rr.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Privilege>().HasData(
                new Privilege { Id = 1, Name = "SeeOwnReservations" },
                new Privilege { Id = 2, Name = "SeeAllRooms" },
                new Privilege { Id = 3, Name = "SeeAllUsers" },
                new Privilege { Id = 4, Name = "SeeAllReservations" },
                new Privilege { Id = 5, Name = "ManageOwnReservationsDates" },
                new Privilege { Id = 6, Name = "ManageOwnReservationsRooms" },
                new Privilege { Id = 7, Name = "ManageAllReservationsDates" },
                new Privilege { Id = 8, Name = "ManageAllReservationsRooms" },
                new Privilege { Id = 9, Name = "ManageAllReservationsStatus" },
                new Privilege { Id = 10, Name = "ManageAllRooms" },
                new Privilege { Id = 11, Name = "ManageAllUsers" }
            );

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Customer" },
                new Role { Id = 2, Name = "Receptionist" },
                new Role { Id = 3, Name = "Administrator" }
            );

            modelBuilder.Entity<RolePrivilege>().HasData(
                new RolePrivilege { RoleId = 1, PrivilegeId = 1 },
                new RolePrivilege { RoleId = 1, PrivilegeId = 2 },
                new RolePrivilege { RoleId = 1, PrivilegeId = 5 },
                new RolePrivilege { RoleId = 1, PrivilegeId = 6 },

                new RolePrivilege { RoleId = 2, PrivilegeId = 2 },
                new RolePrivilege { RoleId = 2, PrivilegeId = 3 },
                new RolePrivilege { RoleId = 2, PrivilegeId = 4 },
                new RolePrivilege { RoleId = 2, PrivilegeId = 7 },
                new RolePrivilege { RoleId = 2, PrivilegeId = 8 },
                new RolePrivilege { RoleId = 2, PrivilegeId = 9 },

                new RolePrivilege { RoleId = 3, PrivilegeId = 2 },
                new RolePrivilege { RoleId = 3, PrivilegeId = 3 },
                new RolePrivilege { RoleId = 3, PrivilegeId = 4 },
                new RolePrivilege { RoleId = 3, PrivilegeId = 7 },
                new RolePrivilege { RoleId = 3, PrivilegeId = 8 },
                new RolePrivilege { RoleId = 3, PrivilegeId = 9 },
                new RolePrivilege { RoleId = 3, PrivilegeId = 10 },
                new RolePrivilege { RoleId = 3, PrivilegeId = 11 }
            );

            modelBuilder.Entity<User>().HasData(
                // Default Admin
                new User {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@gmail.com",
                    Phone = "123 456 789",
                    Password = "$2a$12$5Tu.tWBGVpyJTAq11HjdHeYowvZzvi/izmVX1NS3ISGHdC/Q/dJJG", //zaq1@WSX
                    RoleId = 3
                }
            );

            modelBuilder.Entity<Status>().HasData(
                new Status { Id = 1, Name = "Pending" },
                new Status { Id = 2, Name = "Confirmed" },
                new Status { Id = 3, Name = "Paid" },
                new Status { Id = 4, Name = "Started" },
                new Status { Id = 5, Name = "Completed" },
                new Status { Id = 6, Name = "Cancelled" }
            );
        }
    }
}