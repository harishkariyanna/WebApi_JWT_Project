using Microsoft.EntityFrameworkCore;
using WebApiProject.Models;

namespace WebApiProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Trainer> Trainers { get; set; } = null!;
        public DbSet<GymMember> GymMembers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GymMember>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Name).IsRequired().HasMaxLength(100);
                entity.Property(g => g.Email).IsRequired().HasMaxLength(100);
                entity.Property(g => g.Phone).HasMaxLength(20);
                entity.Property(g => g.Goals).HasMaxLength(500);

                // Default value for JoinedDate for runtime inserts
                entity.Property(g => g.JoinedDate)
                      .HasColumnType("datetime2")
                      .HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(g => g.Category)
                      .WithMany(c => c.GymMembers)
                      .HasForeignKey(g => g.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(g => g.Trainer)
                      .WithMany(t => t.GymMembers)
                      .HasForeignKey(g => g.TrainerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Password).IsRequired();
                entity.Property(u => u.Role).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Description).HasMaxLength(500);
                entity.Property(c => c.Capacity).IsRequired();
            });

            modelBuilder.Entity<Trainer>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
                entity.Property(t => t.Email).IsRequired().HasMaxLength(100);
                entity.Property(t => t.Phone).HasMaxLength(20);
                entity.Property(t => t.Specialization).HasMaxLength(100);

                entity.HasOne(t => t.Category)
                      .WithMany(c => c.Trainers)
                      .HasForeignKey(t => t.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            // Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin1", Password = "admin123", Email = "admin1@example.com", Role = "Admin" },
                new User { Id = 2, Username = "user1", Password = "user123", Email = "user1@example.com", Role = "User" },
                new User { Id = 3, Username = "user2", Password = "user123", Email = "user2@example.com", Role = "User" }
            );

            // Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Yoga", Description = "Yoga sessions for flexibility", Capacity = 20 },
                new Category { Id = 2, Name = "Weightlifting", Description = "Strength training", Capacity = 15 },
                new Category { Id = 3, Name = "Cardio", Description = "High-intensity cardio sessions", Capacity = 25 },
                new Category { Id = 4, Name = "CrossFit", Description = "High-intensity functional training", Capacity = 18 },
                new Category { Id = 5, Name = "Pilates", Description = "Core strength and posture improvement", Capacity = 12 },
                new Category { Id = 6, Name = "Zumba", Description = "Dance-based cardio workout", Capacity = 30 }
            );


            // Trainers
            modelBuilder.Entity<Trainer>().HasData(
                new Trainer { Id = 1, Name = "Ramesh Kumar", Email = "ramesh.kumar@example.com", Specialization = "Yoga", CategoryId = 1 },
                new Trainer { Id = 2, Name = "Anjali Singh", Email = "anjali.singh@example.com", Specialization = "Weightlifting", CategoryId = 2 },
                new Trainer { Id = 3, Name = "Vikram Patel", Email = "vikram.patel@example.com", Specialization = "Cardio", CategoryId = 3 },
                new Trainer { Id = 4, Name = "Sandeep Rao", Email = "sandeep.rao@example.com", Specialization = "CrossFit", CategoryId = 4 },
                new Trainer { Id = 5, Name = "Meera Joshi", Email = "meera.joshi@example.com", Specialization = "Pilates", CategoryId = 5 },
                new Trainer { Id = 6, Name = "Pooja Nair", Email = "pooja.nair@example.com", Specialization = "Zumba", CategoryId = 6 },
                new Trainer { Id = 7, Name = "Amit Sharma", Email = "amit.sharma@example.com", Specialization = "Strength & Conditioning", CategoryId = 2 },
                new Trainer { Id = 8, Name = "Divya Kapoor", Email = "divya.kapoor@example.com", Specialization = "Advanced Yoga", CategoryId = 1 }
            );


            // GymMembers
            modelBuilder.Entity<GymMember>().HasData(
                new GymMember { Id = 1, Name = "Arjun Mehta", Email = "arjun.mehta@example.com", Phone = "9876543210", Goals = "Lose weight", CategoryId = 3, TrainerId = 3, JoinedDate = new DateTime(2025, 07, 24) },
                new GymMember { Id = 2, Name = "Priya Sharma", Email = "priya.sharma@example.com", Phone = "9876501234", Goals = "Increase flexibility", CategoryId = 1, TrainerId = 1, JoinedDate = new DateTime(2025, 08, 13) },
                new GymMember { Id = 3, Name = "Rohit Desai", Email = "rohit.desai@example.com", Phone = "9876512345", Goals = "Build muscle", CategoryId = 2, TrainerId = 2, JoinedDate = new DateTime(2025, 08, 18) },
                new GymMember { Id = 4, Name = "Neha Verma", Email = "neha.verma@example.com", Phone = "9876523456", Goals = "Improve stamina", CategoryId = 3, TrainerId = 3, JoinedDate = new DateTime(2025, 08, 05) },
                new GymMember { Id = 5, Name = "Karan Malhotra", Email = "karan.malhotra@example.com", Phone = "9876534567", Goals = "Reduce stress", CategoryId = 1, TrainerId = 1, JoinedDate = new DateTime(2025, 07, 29) },
                new GymMember { Id = 6, Name = "Sunita Nair", Email = "sunita.nair@example.com", Phone = "9876545678", Goals = "Gain muscle", CategoryId = 2, TrainerId = 2, JoinedDate = new DateTime(2025, 06, 20) },
                new GymMember { Id = 7, Name = "Rajeev Kumar", Email = "rajeev.kumar@example.com", Phone = "9876556789", Goals = "Maintain fitness", CategoryId = 3, TrainerId = 3, JoinedDate = new DateTime(2025, 08, 01) },
                new GymMember { Id = 8, Name = "Anita Shetty", Email = "anita.shetty@example.com", Phone = "9876567890", Goals = "Lose belly fat", CategoryId = 1, TrainerId = 1, JoinedDate = new DateTime(2025, 07, 15) }
            );

        }
    }
}
