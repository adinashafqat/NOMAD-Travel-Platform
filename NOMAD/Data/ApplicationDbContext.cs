using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NOMAD.Models;

namespace NOMAD.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<EmergencyContact> EmergencyContacts { get; set; }
        public DbSet<TravelPreferences> TravelPreferences { get; set; }
        public DbSet<TravelRecord> TravelHistory { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<OfflineCardDb> OfflineCards { get; set; }
        public DbSet<SavedRoute> SavedRoutes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.FullName).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Nationality).HasMaxLength(100);
                entity.Property(e => e.PreferredLanguage).HasMaxLength(50).HasDefaultValue("English");
                entity.Property(e => e.PreferredCurrency).HasMaxLength(10).HasDefaultValue("USD");
                entity.Property(e => e.SerializedVisitedCountries).HasDefaultValue("[]");
            });

            builder.Entity<EmergencyContact>(entity =>
            {
                entity.HasOne(e => e.User)
                    .WithMany(u => u.EmergencyContacts)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Phone).HasMaxLength(50).IsRequired();
            });

            builder.Entity<TravelPreferences>(entity =>
            {
                entity.HasOne(e => e.User)
                    .WithOne(u => u.TravelPreferences)
                    .HasForeignKey<TravelPreferences>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<TravelRecord>(entity =>
            {
                entity.HasOne(e => e.User)
                    .WithMany(u => u.TravelHistory)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.Destination).HasMaxLength(300).IsRequired();
                entity.Property(e => e.TotalExpenses).HasPrecision(18, 2);
            });

            builder.Entity<Trip>(entity =>
            {
                entity.HasOne(t => t.User)
                    .WithMany() 
                    .HasForeignKey(t => t.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                entity.Property(t => t.TotalBudget).HasPrecision(18, 2);
            });

            builder.Entity<Expense>(entity =>
            {
                entity.HasOne(e => e.Trip)
                    .WithMany(t => t.Expenses)
                    .HasForeignKey(e => e.TripId)
                    .OnDelete(DeleteBehavior.Cascade);
                
                entity.Property(e => e.Amount).HasPrecision(18, 2);
            });

            builder.Entity<SavedRoute>(entity =>
            {
                entity.HasOne(s => s.Trip)
                    .WithMany(t => t.SavedRoutes)
                    .HasForeignKey(s => s.TripId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Alert>(entity =>
            {
                entity.HasOne(a => a.User)
                    .WithMany()
                    .HasForeignKey(a => a.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<OfflineCardDb>(entity =>
            {
                entity.HasOne(o => o.User)
                    .WithMany()
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
