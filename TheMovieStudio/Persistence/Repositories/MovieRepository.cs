using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheMovieStudio.Domain.Models;
using TheMovieStudio.Domain.Repositories;
using TheMovieStudio.Persistence.Contexts;

namespace TheMovieStudio.Persistence.Repositories
{
    public class MovieRepository : BaseRepository, IMovieRepository
    {
        public MovieRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<IEnumerable<FilmCopy>> GetAvailableFilmCopiesAsync(int movieId)
        {
            return await _context.FilmCopies.Where(f => f.MovieId == movieId).Where(f => f.IsRented == false).ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int movieId)
        {
            return await _context.Movies.FirstOrDefaultAsync(m => m.MovieId == movieId);
        }
    }
}
