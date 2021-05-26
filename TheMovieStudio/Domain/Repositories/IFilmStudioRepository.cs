using System.Collections.Generic;
using System.Threading.Tasks;
using TheMovieStudio.Domain.Models;

namespace TheMovieStudio.Domain.Repositories
{
    public interface IFilmStudioRepository
    {
        void Add<T>(T entity) where T : class;
        //void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<FilmStudio>> GetAllFilmStudiosAsync();
        Task<FilmStudio> GetFilmStudioByIdAsync(int FilmStudioid);
        Task<IEnumerable<FilmCopy>> GetRentedFilmCopiesAsync(int FilmStudioid);
    }
}
