using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Services.Rentals;
using MovieStore.Services.Rentals.Dto;
//using MovieStore.Api.Models;

namespace MovieStore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        public RentalController(IRentalService rentalService)
        {
            this._rentalService = rentalService;
        }

        // GET api/rental
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<RentalDto>>> GetRentals()
        {
            var rentals = await _rentalService.GetAllWithMovie();
            return Ok(rentals);
        }

        // GET api/rental/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RentalDto>> GetRentalById(int id)
        {
            try
            {
                var rental = await _rentalService.GetRentalById(id);
                return Ok(rental);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // POST api/rental
        [HttpPost("")]
        public async Task<IActionResult> PostRental(RentalDto rental)
        {
            try
            {
                await _rentalService.CreateRental(rental);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT api/rental/5
        [HttpPut("")]
        public async Task<IActionResult> PutRental(RentalDto rental)
        {
            try
            {
                await _rentalService.UpdateRental(rental);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE api/rental/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentalById(int id)
        {
            try
            {
                var rental = await _rentalService.GetRentalById(id);
                if (rental != null)
                {
                    await _rentalService.DeleteRental(rental);
                    return Ok();
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // POST api/rental/RemoveMultiple
        [HttpPost]
        [Route("RemoveMultiple")]
        public async Task<IActionResult> DeleteRentals(IEnumerable<int> rentalIds)
        {
            try
            {

                await _rentalService.DeleteRentals(rentalIds);
                return Ok();

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}