using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Services.Movies;
using MovieStore.Services.Movies.Dto;
using MovieStore.Services.Rentals;
//using MovieStore.Api.Models;

namespace MovieStore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly IMovieService _movieService;
        private readonly IRentalService _rentalService;

        public MoviesController(IMovieService movieService, IRentalService rentalService)
        {
            this._movieService = movieService;
            this._rentalService = rentalService;
        }

        // GET api/movies
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetAllMovies()
        {
            var movies = await _movieService.GetAllWithGenre();
            return Ok(movies);
        }

        // GET api/movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDto>> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieById(id);
            return Ok(movie);
        }

        // POST api/movies
        [HttpPost("")]
        public async Task<IActionResult> PostMovie(MovieDto newMovie)
        {
            try
            {
                await _movieService.CreateMovie(newMovie);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT api/movies
        [HttpPut("")]
        public async Task<IActionResult> PutMovie(MovieDto movie)
        {
            try
            {
                _ = await _movieService.UpdateMovie(movie);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE api/movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovieById(int id)
        {
            try
            {
                if (await _rentalService.ExistRentalWithMovieIds(new List<int>() { id }))
                    return Problem("Este filme possui locação cadastrada! Não será possível excluir!");
                var movie = await _movieService.GetMovieById(id);
                if (movie != null)
                {
                    await _movieService.DeleteMovie(movie);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // POST api/movies/RemoveMultiple
        [HttpPost]
        [Route("RemoveMultiple")]
        public async Task<IActionResult> DeleteMovies(IEnumerable<int> movieIds)
        {
            try
            {
                if (await _rentalService.ExistRentalWithMovieIds(movieIds))
                    return Problem("Um ou mais filmes selecionados possuem locação cadastrada! Não será possível excluir!");
                await _movieService.DeleteMovies(movieIds);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}