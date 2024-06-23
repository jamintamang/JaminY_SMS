using JaminY_SMS.Models;
using JaminY_SMS.Repositories.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JaminY_SMS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            :base(options)
        {
            
        }
        public DbSet<Course> Course { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<ApplicationUser>()
                .Property(e => e.MiddleName)
                .HasMaxLength(255);

            builder.Entity<ApplicationUser>()
                .Property(e => e.LastName)
                .HasMaxLength(255)
                .IsRequired();


            builder.Entity<ApplicationUser>()
                .Property(e => e.Address)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<ApplicationUser>()
                .Property(e => e.UserRoleId)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<ApplicationUser>()
                .Property(e => e.IsActive)
                .HasDefaultValue(true);


            builder.Entity<ApplicationUser>()
                .Property(e => e.PictureUrl)
                .HasMaxLength(255);

            builder.Entity<ApplicationUser>()
                .Property(e => e.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");


            builder.Entity<IdentityRole>()
                .ToTable("Roles")
                .Property(p => p.Id)
                .HasColumnName("RoleId");


            builder.ApplyConfiguration(new CourseConfiguration());
            builder.ApplyConfiguration(new StudentConfiguration());
            
        }
    }
}
