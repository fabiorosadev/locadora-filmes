using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieStore.Core.Models.MovieRentals;

namespace MovieStore.Data.Configurations
{
    public class MovieRentalConfiguration : IEntityTypeConfiguration<MovieRental>
    {
        public void Configure(EntityTypeBuilder<MovieRental> builder)
        {
            builder
                .HasKey(mr => new { mr.MovieId, mr.RentalId });

            builder
                .HasOne(mr => mr.Movie)
                .WithMany(m => m.MovieRentals)
                .HasForeignKey(mr => mr.MovieId);

            builder
                .HasOne(mr => mr.Rental)
                .WithMany(r => r.MovieRentals)
                .HasForeignKey(mr => mr.RentalId);

            builder
                .ToTable("MovieRentals");
        }
    }
}