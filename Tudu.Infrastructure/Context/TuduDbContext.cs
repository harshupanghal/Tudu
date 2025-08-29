using Microsoft.EntityFrameworkCore;
using Tudu.Domain.Entities;

namespace Tudu.Infrastructure.Context
    {
    public class TuduDbContext : DbContext
        {
        public TuduDbContext(DbContextOptions<TuduDbContext> options)
            : base(options)
            {
            // Ensure DB and tables are created at startup
            //Database.EnsureCreated();
            }

        // DbSets for entities
        public DbSet<User> Users { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            base.OnModelCreating(modelBuilder);

            // User Configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.UserName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Password).IsRequired();
                entity.Property(u => u.CreatedAt).IsRequired();
            });

            // Task Configuration
            modelBuilder.Entity<UserTask>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Title).IsRequired().HasMaxLength(200);
                entity.Property(t => t.IsCompleted).IsRequired();
                entity.Property(t => t.CreatedAt).IsRequired();

                // Relationships
                entity.HasOne(t => t.User)
                      .WithMany(u => u.Tasks)
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            }
        }
    }
