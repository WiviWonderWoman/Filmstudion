using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TheMovieStudio.Domain.Models;
using TheMovieStudio.Domain.Services;
using TheMovieStudio.Resources.Public;

namespace TheMovieStudio.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class PublicController : Controller
    {
        private readonly IFilmStudioService _filmStudioService;
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public PublicController(IFilmStudioService filmStudioService, IMovieService movieService, IMapper mapper)
        {
            _filmStudioService = filmStudioService;
            _movieService = movieService;
            _mapper = mapper;
        }

        [HttpGet("filmstudios")]
        public async Task<ActionResult<PublicFilmStudioResource[]>> GetAllFilmStudiosAsync()
        {
            try
            {
                var filmStudios = await _filmStudioService.GetAllFilmStudiosAsync();
                return _mapper.Map<PublicFilmStudioResource[]>(filmStudios);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("filmstudios/{filmStudioId}")]
        public async Task<ActionResult<PublicFilmStudioResource>> GetFilmStudioByIdAsync(int filmStudioId)
        {
            try
            {
                var filmStudio = await _filmStudioService.GetFilmStudioByIdAsync(filmStudioId);
                if (filmStudio == null)
                {
                    return NotFound($"Could not find filmstudio with id of {filmStudioId}");
                }
                return _mapper.Map<PublicFilmStudioResource>(filmStudio);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("movies")]
        public async Task<ActionResult<PublicMovieResource[]>> GetAllMoviesAsync()
        {
            try
            {
                var movies = await _movieService.GetAllMoviesAsync();
                return _mapper.Map<PublicMovieResource[]>(movies);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet("movies/{movieId}")]
        public async Task<ActionResult<PublicMovieResource>> GetMovieByIdAsync(int movieId)
        {
            try
            {
                var movie = await _movieService.GetMovieByIdAsync(movieId);
                if (movie == null)
                {
                    return NotFound($"Could not find movie with id of {movieId}");
                }
                return _mapper.Map<Movie, PublicMovieResource>(movie);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
