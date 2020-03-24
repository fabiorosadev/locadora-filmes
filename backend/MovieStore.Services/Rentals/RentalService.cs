using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieStore.Core;
using MovieStore.Core.Models.MovieRentals;
using MovieStore.Core.Models.Rentals;
using MovieStore.Services.Rentals.Dto;

namespace MovieStore.Services.Rentals
{
    public class RentalService : IRentalService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RentalService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<RentalDto> CreateRental(RentalDto rental)
        {
            var entityRental = ObjectMapper.Mapper.Map<Rental>(rental);
            await _unitOfWork.Rentals.AddAsync(entityRental);
            await _unitOfWork.CommitAsync();
            return ObjectMapper.Mapper.Map<RentalDto>(entityRental);
        }

        public async Task DeleteRental(RentalDto rental)
        {
            var entityRental = await _unitOfWork.Rentals.GetByIdAsync(rental.Id);
            _unitOfWork.Rentals.Remove(entityRental);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteRentals(IEnumerable<int> rentalIds)
        {
            var rentals = _unitOfWork.Rentals.Find(r => rentalIds.Any(i => i == r.Id));
            _unitOfWork.Rentals.RemoveRange(rentals);
            await _unitOfWork.CommitAsync();
        }

        public async Task<bool> ExistRentalWithMovieIds(IEnumerable<int> movieIds)
        {
            return await Task.FromResult(_unitOfWork.MovieRentals.Find(mr => movieIds.Any(i => i == mr.MovieId)).Any());
        }

        public async Task<IEnumerable<RentalDto>> GetAllWithMovie()
        {
            return ObjectMapper.Mapper.Map<IEnumerable<RentalDto>>(await _unitOfWork.Rentals.GetAllWithMoviesAsync());
        }

        public async Task<RentalDto> GetRentalById(int id)
        {
            return ObjectMapper.Mapper.Map<RentalDto>(await _unitOfWork.Rentals.GetWithMoviesByIdAsync(id));
        }

        public async Task<IEnumerable<RentalDto>> GetRentalsByCustomerCpf(string customerCpf)
        {
            return ObjectMapper.Mapper.Map<IEnumerable<RentalDto>>(await _unitOfWork.Rentals.GetAllWithMoviesByCustomerCpfAsync(customerCpf));
        }

        public async Task<IEnumerable<RentalDto>> GetRentalsByMovieId(int movieId)
        {
            return ObjectMapper.Mapper.Map<IEnumerable<RentalDto>>(await _unitOfWork.Rentals.GetAllWithMoviesByMovieIdAsync(movieId));
        }

        public async Task<RentalDto> UpdateRental(RentalDto rental)
        {
            var entityRental = await _unitOfWork.Rentals.GetWithMoviesByIdAsync(rental.Id);
            entityRental = ObjectMapper.Mapper.Map(rental, entityRental);
            await _unitOfWork.CommitAsync();
            return ObjectMapper.Mapper.Map<RentalDto>(entityRental);
        }
    }
}