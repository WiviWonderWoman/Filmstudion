using System.Collections.Generic;
using System.Threading.Tasks;
using TheMovieStudio.Domain.Models;

namespace TheMovieStudio.Domain.Repositories
{
    public interface IMovieRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Movie>> GetAllMoviesAsync();
        Task<Movie> GetMovieByIdAsync(int MovieId);
        Task<IEnumerable<FilmCopy>> GetAvailableFilmCopiesAsync(int MovieId);
    }
}
