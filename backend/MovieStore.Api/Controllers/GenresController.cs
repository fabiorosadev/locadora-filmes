using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Services.Genres;
using MovieStore.Services.Genres.Dto;
using MovieStore.Services.Movies;

namespace MovieStore.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly IMovieService _movieService;
        public GenresController(IGenreService genreService, IMovieService movieService)
        {
            this._genreService = genreService;
            this._movieService = movieService;
        }

        // GET api/genres
        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetGenres()
        {
            var genres = await _genreService.GetAllGenres();
            return Ok(genres);
        }

        // GET api/genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetGenreById(int id)
        {
            var genre = await _genreService.GetGenreById(id);
            return Ok(genre);
        }

        // POST api/genres
        [HttpPost("")]
        public async Task<IActionResult> PostGenre(GenreDto genre)
        {
            try
            {
                await _genreService.CreateGenre(genre);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // PUT api/genres/5
        [HttpPut("")]
        public async Task<IActionResult> PutGenre(GenreDto genre)
        {
            try
            {
                await _genreService.UpdateGenre(genre);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // DELETE api/genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenreById(int id)
        {
            try
            {
                if (await _movieService.ExistMoviesWithGenreIds(new List<int>() { id }))
                    return Problem("Este gênero possui filme cadastrado! Não é possível excluir!");
                var genre = await _genreService.GetGenreById(id);
                if (genre != null)
                {
                    await _genreService.DeleteGenre(genre);
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // POST api/genres/RemoveMultiple
        [HttpPost]
        [Route("RemoveMultiple")]
        public async Task<IActionResult> DeleteGenres(IEnumerable<int> genreIds)
        {
            try
            {
                if (await _movieService.ExistMoviesWithGenreIds(genreIds))
                    return Problem("Um ou mais gênero selecionado possui filme cadastrado! Não será possível excluir!");
                await _genreService.DeleteGenres(genreIds);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}