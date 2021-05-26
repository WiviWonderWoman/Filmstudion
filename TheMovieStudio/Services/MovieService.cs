using System.Collections.Generic;
using System.Threading.Tasks;
using TheMovieStudio.Domain.Models;
using TheMovieStudio.Domain.Repositories;
using TheMovieStudio.Domain.Services;

namespace TheMovieStudio.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void Add<T>(T entity) where T : class
        {
            _movieRepository.Add(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _movieRepository.SaveChangesAsync());
        }

        public void Delete<T>(T entity) where T : class
        {
            _movieRepository.Delete(entity);
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _movieRepository.GetAllMoviesAsync();
        }

        public async Task<IEnumerable<FilmCopy>> GetAvailableFilmCopiesAsync(int movieId)
        {
            return await _movieRepository.GetAvailableFilmCopiesAsync(movieId);
        }

        public async Task<Movie> GetMovieByIdAsync(int movieId)
        {
            return await _movieRepository.GetMovieByIdAsync(movieId);
        }
    }
}
