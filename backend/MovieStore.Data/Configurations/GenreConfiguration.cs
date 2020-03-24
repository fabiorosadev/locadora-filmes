using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Core.Models.Genres;

namespace MovieStore.Data.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder
                .HasKey(g => g.Id);

            builder
                .Property(g => g.Id)
                .UseIdentityColumn();

            builder
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(g => g.CreationDate)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Property(g => g.Status)
                .HasDefaultValue(GenreStatusEnum.Active);

            builder
                .ToTable("Genres");
        }
    }
}