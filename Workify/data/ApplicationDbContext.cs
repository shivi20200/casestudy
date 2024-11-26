using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using Workify.Models;

namespace Workify.Data;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var configSection = configBuilder.GetSection("ConnectionStrings");
        var conStr = configSection["ConStr"] ?? null;

        optionsBuilder.UseSqlServer(conStr);
    }
    public DbSet<User> Users { get; set; }
        public DbSet<Employer> Employers { get; set; }
        public DbSet<JobSeeker> JobSeekers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobListing> JobListings { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Resume> Resumes { get; set; }
        public DbSet<SearchHistory> SearchHistories { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        //  // Configuring the one-to-one relationship between User and Employer or JobSeeker
        //  modelBuilder.Entity<User>()
        //      .HasOne(u => u.Employer)
        //      .WithOne(e => e.User)
        //      .HasForeignKey<Employer>(e => e.UserId)
        //      .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for Employer

        //  modelBuilder.Entity<User>()
        //      .HasOne(u => u.JobSeeker)
        //      .WithOne(j => j.User)
        //      .HasForeignKey<JobSeeker>(j => j.UserId)
        //      .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for JobSeeker

        //  // Employer to Company relationship: Many-to-One
        //  modelBuilder.Entity<Employer>()
        //      .HasOne(e => e.Company)
        //      .WithMany(c => c.Employers)
        //      .HasForeignKey(e => e.CompanyId)
        //      .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for Employer

        //  // JobSeeker to Resume relationship: One-to-Many
        //  modelBuilder.Entity<JobSeeker>()
        //      .HasMany(j => j.Resumes)
        //      .WithOne(r => r.JobSeeker)
        //      .HasForeignKey(r => r.SeekerId)
        //      .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for Resumes

        //  // JobSeeker to Application relationship: One-to-Many
        //  modelBuilder.Entity<JobSeeker>()
        //      .HasMany(j => j.Applications)
        //      .WithOne(a => a.JobSeeker)
        //      .HasForeignKey(a => a.SeekerId)
        //      .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for Applications

        //  // JobSeeker to SearchHistory relationship: One-to-Many
        //  modelBuilder.Entity<JobSeeker>()
        //      .HasMany(j => j.SearchHistories)
        //      .WithOne(s => s.JobSeeker)
        //      .HasForeignKey(s => s.SeekerId)
        //      .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for SearchHistory

        //  // JobListing to Employer relationship: Many-to-One
        //  modelBuilder.Entity<JobListing>()
        //      .HasOne(j => j.Employer)
        //      .WithMany(e => e.JobListings)
        //      .HasForeignKey(j => j.EmployerId)
        //      .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for JobListings

        //  // JobListing to Company relationship: Many-to-One
        ////  modelBuilder.Entity<JobListing>()
        //  //    .HasOne(j => j.Company)
        //    //  .WithMany(c => c.JobListings)
        //     // .HasForeignKey(j => j.CompanyId)
        //     // .OnDelete(DeleteBehavior.SetNull);  // SetNull on delete for Company

        //  // Application to JobListing relationship: Many-to-One
        //  modelBuilder.Entity<Application>()
        //      .HasOne(a => a.JobListing)
        //      .WithMany(j => j.Applications)
        //      .HasForeignKey(a => a.JobId)
        //      .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for Applications

        //  // Application to JobSeeker relationship: Many-to-One
        //  modelBuilder.Entity<Application>()
        //      .HasOne(a => a.JobSeeker)
        //      .WithMany(j => j.Applications)
        //      .HasForeignKey(a => a.SeekerId)
        //      .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for Applications

        //  // Resume to JobSeeker relationship: Many-to-One
        //  modelBuilder.Entity<Resume>()
        //      .HasOne(r => r.JobSeeker)
        //      .WithMany(j => j.Resumes)
        //      .HasForeignKey(r => r.SeekerId)
        //      .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for Resumes

        //  // SearchHistory to JobSeeker relationship: Many-to-One
        //  modelBuilder.Entity<SearchHistory>()
        //      .HasOne(s => s.JobSeeker)
        //      .WithMany(j => j.SearchHistories)
        //      .HasForeignKey(s => s.SeekerId)
        //      .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for SearchHistory

        //  // Other custom configurations like indexes, unique constraints, etc.
        //  modelBuilder.Entity<User>()
        //      .HasIndex(u => u.Email)
        //      .IsUnique();  // Ensure Email is unique

        //  // Example for defining string lengths (optional)
        //  modelBuilder.Entity<User>()
        //      .Property(u => u.Name)
        //      .HasMaxLength(100);

        //  modelBuilder.Entity<User>()
        //      .Property(u => u.Email)
        //      .HasMaxLength(100);

        //  modelBuilder.Entity<User>()
        //      .Property(u => u.Password)
        //      .HasMaxLength(100);
        // User -> Employer (One-to-One)
        modelBuilder.Entity<Employer>()
            .HasOne(e => e.User)
            .WithOne(u => u.Employer)
            .HasForeignKey<Employer>(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> JobSeeker (One-to-One)
        modelBuilder.Entity<JobSeeker>()
            .HasOne(js => js.User)
            .WithOne(u => u.JobSeeker)
            .HasForeignKey<JobSeeker>(js => js.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Employer -> Company (Many-to-One)
        modelBuilder.Entity<Employer>()
            .HasOne(e => e.Company)
            .WithMany(c => c.Employers)
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        // Employer -> JobListing (One-to-Many)
        modelBuilder.Entity<JobListing>()
            .HasOne(j => j.Employer)
            .WithMany(e => e.JobListings)
            .HasForeignKey(j => j.EmployerId)
            .OnDelete(DeleteBehavior.Restrict);

        // JobSeeker -> Application (One-to-Many)
        modelBuilder.Entity<Application>()
            .HasOne(a => a.JobSeeker)
            .WithMany(js => js.Applications)
            .HasForeignKey(a => a.SeekerId)
          
            .OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete

        // JobListing -> Application (One-to-Many)
        modelBuilder.Entity<Application>()
            .HasOne(a => a.JobListing)
            .WithMany(j => j.Applications)
            .HasForeignKey(a => a.JobId)
            .OnDelete(DeleteBehavior.Restrict); // Avoid cascade delete

        // JobSeeker -> Resume (One-to-Many)
        modelBuilder.Entity<Resume>()
            .HasOne(r => r.JobSeeker)
            .WithMany(js => js.Resumes)
            .HasForeignKey(r => r.SeekerId)
            .OnDelete(DeleteBehavior.Cascade);

        // JobSeeker -> SearchHistory (One-to-Many)
        modelBuilder.Entity<SearchHistory>()
            .HasOne(sh => sh.JobSeeker)
            .WithMany(js => js.SearchHistories)
            .HasForeignKey(sh => sh.SeekerId)
            .OnDelete(DeleteBehavior.Cascade);
        // Table Configurations
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Employer>().ToTable("Employers");
        modelBuilder.Entity<JobSeeker>().ToTable("JobSeekers");
        modelBuilder.Entity<Company>().ToTable("Companies");
        modelBuilder.Entity<JobListing>().ToTable("JobListings");
        modelBuilder.Entity<Application>().ToTable("Applications");
        modelBuilder.Entity<Resume>().ToTable("Resumes");
        modelBuilder.Entity<SearchHistory>().ToTable("SearchHistories");

        // Constraints and Column Configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.UserId);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Password).IsRequired();
            entity.Property(u => u.Role).IsRequired();
        });

        modelBuilder.Entity<Employer>(entity =>
        {
            entity.HasKey(e => e.EmployerId);
        });

        modelBuilder.Entity<JobSeeker>(entity =>
        {
            entity.HasKey(js => js.SeekerId);
            entity.Property(js => js.ProfileSummary).HasMaxLength(500);
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(c => c.CompanyId);
            entity.Property(c => c.CompanyName).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<JobListing>(entity =>
        {
            entity.HasKey(j => j.JobId);
            entity.Property(j => j.Title).IsRequired().HasMaxLength(100);
            entity.Property(j => j.Qualifications).IsRequired().HasMaxLength(1000);
            entity.Property(j => j.Description).IsRequired().HasMaxLength(2000);
            entity.Property(j => j.Location).IsRequired().HasMaxLength(200);
            entity.Property(j => j.Salary).HasPrecision(18, 2); ;
        });

        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(a => a.ApplicationId);
            entity.Property(a => a.Status).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Resume>(entity =>
        {
            entity.HasKey(r => r.ResumeId);
        });

        modelBuilder.Entity<SearchHistory>(entity =>
        {
            entity.HasKey(sh => sh.SearchId);
            entity.Property(sh => sh.SearchCriteria).IsRequired().HasMaxLength(200);
        });

        base.OnModelCreating(modelBuilder);
    }
}
    
        
    
   