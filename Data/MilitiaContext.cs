using Microsoft.EntityFrameworkCore;
using MilitiaDuty.Models.Assignments;
using MilitiaDuty.Models.DutyDates;
using MilitiaDuty.Models.Militias;
using MilitiaDuty.Models.Rules;

namespace MilitiaDuty.Data
{
    public class MilitiaContext : DbContext
    {
        public DbSet<Militia> Militias { get; set; }
        public DbSet<DutyDate> DutyDates { get; set; }
        public DbSet<MilitiaDutyDate> MilitiaDutyDates { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<MilitiaRule> MilitiaRules { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Models.Assignments.Task> Tasks { get; set; }
        public DbSet<RuleTask> RuleTasks { get; set; }
        public DbSet<Shift> Shifts { get; set; }

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

            // config many to many between Rule and Militia
            modelBuilder.Entity<Rule>()
                .HasMany(r => r.Militias)
                .WithMany(m => m.Rules)
                .UsingEntity<MilitiaRule>(
                    mr => mr
                        .HasOne<Militia>()
                        .WithMany()
                        .HasForeignKey(mr => mr.MilitiaId),
                    mr => mr
                        .HasOne<Rule>()
                        .WithMany()
                        .HasForeignKey(mr => mr.RuleId),
                    mr =>
                    {
                        mr.HasKey(mr => new { mr.MilitiaId, mr.RuleId });
                    }
                );

            // config many to many between Rule and Task
            modelBuilder.Entity<Rule>()
                .HasMany(r => r.Tasks)
                .WithMany(t => t.Rules)
                .UsingEntity<RuleTask>(
                    rt => rt
                        .HasOne<Models.Assignments.Task>()
                        .WithMany()
                        .HasForeignKey(rt => rt.TaskId),
                    rt => rt
                        .HasOne<Rule>()
                        .WithMany()
                        .HasForeignKey(mr => mr.RuleId),
                    rt =>
                    {
                        rt.HasKey(mr => new { mr.TaskId, mr.RuleId });
                    }
                );
        }
        public DbSet<MilitiaDuty.Models.Assignments.Shift> Shift { get; set; } = default!;
    }
}
