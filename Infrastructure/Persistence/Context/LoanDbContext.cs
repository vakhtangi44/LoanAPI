using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context
{
    public class LoanDbContext : DbContext
    {
        public LoanDbContext(DbContextOptions<LoanDbContext> options)
            : base(options) { }

        public DbSet<Accountant> Accountants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations for entities
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new LoanConfiguration());
            modelBuilder.ApplyConfiguration(new AccountantConfiguration());

            // Configuration for ErrorLog table
            modelBuilder.Entity<ErrorLog>(entity =>
            {
                entity.ToTable("ErrorLogs"); // Explicit table name
                entity.HasKey(e => e.Id);    // Primary key
                entity.Property(e => e.Message)
                      .IsRequired()
                      .HasMaxLength(1000);
                entity.Property(e => e.StackTrace)
                      .HasMaxLength(4000);
                entity.Property(e => e.CreatedAt)
                      .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}