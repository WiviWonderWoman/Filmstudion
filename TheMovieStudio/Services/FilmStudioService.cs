using System.Collections.Generic;
using System.Threading.Tasks;
using TheMovieStudio.Domain.Models;
using TheMovieStudio.Domain.Repositories;
using TheMovieStudio.Domain.Services;

namespace TheMovieStudio.Services
{
    public class FilmStudioService : IFilmStudioService
    {
        private readonly IFilmStudioRepository _filmStudioRepository;
        public FilmStudioService(IFilmStudioRepository filmStudioRepository)
        {
            _filmStudioRepository = filmStudioRepository;
        }

        public void Add<T>(T entity) where T : class
        {
            _filmStudioRepository.Add(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _filmStudioRepository.SaveChangesAsync());
        }

        public async Task<IEnumerable<FilmStudio>> GetAllFilmStudiosAsync()
        {
            return await _filmStudioRepository.GetAllFilmStudiosAsync();
        }

        public async Task<FilmStudio> GetFilmStudioByIdAsync(int filmStudioid)
        {
            return await _filmStudioRepository.GetFilmStudioByIdAsync(filmStudioid);
        }

        public async Task<IEnumerable<FilmCopy>> GetRentedFilmCopiesAsync(int filmStudioid)
        {
            return await _filmStudioRepository.GetRentedFilmCopiesAsync(filmStudioid);
        }
    }
}
