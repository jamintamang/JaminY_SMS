using JaminY_SMS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JaminY_SMS.Repositories.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
               .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(true);

            builder.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(true);

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(true);

            builder.Property(e => e.Gender)
                .HasMaxLength(30)
                .IsUnicode(true);

            builder.Property(e => e.Address)
                .HasMaxLength(30)
               .IsUnicode(true);

            builder.Property(e => e.Class)
                .HasMaxLength(30)
               .IsUnicode(true);

            builder.Property(e => e.Section)
                .HasMaxLength(30)
               .IsUnicode(true);

            builder.Property(e => e.ImageUrl)
                .HasMaxLength(500)
                .IsUnicode(true);
        }
    }
}
