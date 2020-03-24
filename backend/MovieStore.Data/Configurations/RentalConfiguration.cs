using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Core.Models.Rentals;

namespace MovieStore.Data.Configurations
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder
                .HasKey(r => r.Id);

            builder
                .Property(r => r.Id)
                .UseIdentityColumn();

            builder
                .Property(r => r.CustomerCpf)
                .IsRequired()
                .HasMaxLength(14);

            builder
                .Property(r => r.RentalDate)
                .IsRequired();

            builder
                .ToTable("Rentals");
        }
    }
}