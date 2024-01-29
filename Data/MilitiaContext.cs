using Microsoft.EntityFrameworkCore;
using MilitiaDuty.Models.DutyDates;
using MilitiaDuty.Models.Militias;

namespace MilitiaDuty.Data
{
    public class MilitiaContext : DbContext
    {
        public DbSet<Militia> Militias { get; set; }
        public DbSet<DutyDate> DutyDates { get; set; }
        public DbSet<MilitiaDutyDate> MilitiaDutyDates { get; set; }

        public string DbPath { get; }

        public MilitiaContext()
        {
            var folder = Environment.SpecialFolder.UserProfile;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "militia.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // config many to many between DutyDate and Militia
            modelBuilder.Entity<DutyDate>()
                .HasMany(d => d.Militias)
                .WithMany(m => m.DutyDates)
                .UsingEntity<MilitiaDutyDate>(
                    md => md
                        .HasOne<Militia>()
                        .WithMany()
                        .HasForeignKey(md => md.MilitiaId),
                    md => md
                        .HasOne<DutyDate>()
                        .WithMany()
                        .HasForeignKey(md => md.DutyDateId),
                    md =>
                    {
                        md.HasKey(md => new { md.MilitiaId, md.DutyDateId });
                    }
                );
        }
    }
}
