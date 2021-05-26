using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TheMovieStudio.Domain.Models;
using TheMovieStudio.Domain.Services;
using TheMovieStudio.Resources;
using TheMovieStudio.Resources.Movies;

namespace TheMovieStudio.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class FilmCopiesController : Controller
    {
        private readonly IFilmCopyService _filmCopyService;
        private readonly IFilmStudioService _filmStudioService;
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public FilmCopiesController(IFilmCopyService filmCopyService,
            IFilmStudioService filmStudioService, IMovieService movieService,
            IUserService userService, IMapper mapper)
        {
            _filmCopyService = filmCopyService;
            _filmStudioService = filmStudioService;
            _movieService = movieService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult<MovieResource>> GetMovieByIdWithAvailableFilmCopiesAsync(int movieId)
        {

            try
            {
                var movie = await _movieService.GetMovieByIdAsync(movieId);

                if (movie == null)
                {
                    return NotFound($"Could not find movie with id of {movieId}");
                }

                var filmCopies = await _filmCopyService.GetAllFilmCopiesByMovieIdAsync(movieId);
                var available = filmCopies.Where(f => f.IsRented == false);

                var result = _mapper.Map<Movie, MovieResource>(movie);

                result.AvailableFilmcopies = available;

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpGet]
        public async Task<ActionResult<FilmCopyResource[]>> GetAllRentedFilmCopiesAsync()
        {
            try
            {
                var userName = User.Identity.Name;
                var userId = int.Parse(userName);
                var user = _userService.GetById(userId);

                if (user.IsAdmin)
                {
                    var rented = await _filmCopyService.GetAllRentedFilmCopiesAsync();
                    var filmcopies = _mapper.Map<FilmCopyResource[]>(rented);

                    foreach (var copie in filmcopies)
                    {
                        var movie = await _movieService.GetMovieByIdAsync(copie.MovieId);
                        copie.MovieName = movie.Title;
                    }
                    return filmcopies;
                }

                else if (!user.IsAdmin && user.FilmStudioId != 0)
                {
                    var filmStudioId = user.FilmStudioId;

                    var rented = await _filmStudioService.GetRentedFilmCopiesAsync(filmStudioId);
                    var filmcopies = _mapper.Map<FilmCopyResource[]>(rented);

                    if (rented == null)
                    {
                        return NotFound($"You have no rented movies.");
                    }

                    foreach (var copie in filmcopies)
                    {
                        var movie = await _movieService.GetMovieByIdAsync(copie.MovieId);
                        copie.MovieName = movie.Title;
                    }

                    return filmcopies;
                }

                return Unauthorized("You are not allowed this action.");
            }

            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpPut("{movieId}/rent")]
        public async Task<ActionResult<FilmCopyResource>> RentFilmCopyAsync(int movieId)
        {
            try
            {
                var userName = User.Identity.Name;
                var userId = int.Parse(userName);
                var user = _userService.GetById(userId);

                if (user.IsAdmin) { return Unauthorized("Only FilmStudios are allowed this action."); }

                var filmCopies = await _filmStudioService.GetRentedFilmCopiesAsync(userId);
                var rentedFilmCopy = filmCopies.FirstOrDefault(f => f.MovieId == movieId);

                if (rentedFilmCopy != null) { return BadRequest($"You already rented a copy of the movie with MovieId of {movieId}"); }

                var filmCopy = await _filmCopyService.GetAvaibleFilmCopyByMovieIdAsync(movieId);

                if (filmCopy == null) { return NotFound($"Could not find avaible filmcopy with MovieId of {movieId}"); }

                filmCopy.IsRented = true;
                filmCopy.FilmStudioId = user.FilmStudioId;

                var resource = _mapper.Map<FilmCopyResource>(filmCopy);
                var movie = await _movieService.GetMovieByIdAsync(resource.MovieId);
                resource.MovieName = movie.Title;

                if (await _filmCopyService.SaveChangesAsync()) { return Created("", resource); }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();
        }

        [HttpPut("{movieId}/return")]
        public async Task<ActionResult<FilmCopyResource>> ReturnFilmCopyAsync(int movieId)
        {
            try
            {
                var userName = User.Identity.Name;
                var userId = int.Parse(userName);
                var user = _userService.GetById(userId);

                if (user.IsAdmin) { return Unauthorized("Only FilmStudios are allowed this action."); }

                var filmCopies = await _filmStudioService.GetRentedFilmCopiesAsync(user.FilmStudioId);
                var filmCopy = filmCopies.FirstOrDefault(f => f.MovieId == movieId);

                if (filmCopy == null) { return NotFound($"Could not find rented filmcopy with MovieId of {movieId}"); }

                filmCopy.IsRented = false;
                filmCopy.FilmStudioId = 0;

                var resource = _mapper.Map<FilmCopyResource>(filmCopy);
                var movie = await _movieService.GetMovieByIdAsync(resource.MovieId);
                resource.MovieName = movie.Title;

                if (await _filmCopyService.SaveChangesAsync()) { return Created("", resource); }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return BadRequest();
        }
    }
}
