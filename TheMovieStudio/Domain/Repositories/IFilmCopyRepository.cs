using System.Collections.Generic;
using System.Threading.Tasks;
using TheMovieStudio.Domain.Models;

namespace TheMovieStudio.Domain.Repositories
{
    public interface IFilmCopyRepository
    {
        void CreateCopies(int filmCopies, int movieId);
        void CreateCopies(int oldCopies, int newCopies, int movieId);
        void DeleteCopies(int amount, IEnumerable<FilmCopy> filmCopies);
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<FilmCopy>> GetAllRentedFilmCopiesAsync();
        Task<IEnumerable<FilmCopy>> GetAllFilmCopiesByMovieIdAsync(int movieId);
        Task<FilmCopy> GetAvaibleFilmCopyByMovieIdAsync(int movieId);
    }
}
