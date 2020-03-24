using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Services.Rentals.Dto;

namespace MovieStore.Services.Rentals
{
    public interface IRentalService
    {
        Task<IEnumerable<RentalDto>> GetAllWithMovie();
        Task<RentalDto> GetRentalById(int id);
        Task<IEnumerable<RentalDto>> GetRentalsByMovieId(int movieId);
        Task<bool> ExistRentalWithMovieIds(IEnumerable<int> movieIds);
        Task<IEnumerable<RentalDto>> GetRentalsByCustomerCpf(string customerCpf);
        Task<RentalDto> CreateRental(RentalDto rental);
        Task<RentalDto> UpdateRental(RentalDto rental);
        Task DeleteRental(RentalDto rental);
        Task DeleteRentals(IEnumerable<int> rentalIds);

    }
}