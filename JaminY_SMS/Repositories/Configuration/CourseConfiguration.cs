using JaminY_SMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JaminY_SMS.Repositories.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
               .ValueGeneratedOnAdd();

            builder.Property(e => e.CourseName)
                .HasMaxLength(200)
                .IsUnicode(true);

            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(true);

            builder.HasMany(e => e.Students)
                .WithOne(pt => pt.Course)
                .HasForeignKey(e => e.CourseId);

            builder.Property(e => e.IsActive)
           .HasDefaultValue(true);
        }
    }
}
