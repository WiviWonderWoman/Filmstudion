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
    public class FilmCopyRepository : BaseRepository, IFilmCopyRepository
    {
        public FilmCopyRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
        { }

        public void CreateCopies(int filmCopies, int movieId)
        {
            for (double i = 0.0; i < filmCopies; i++)
            {
                double partId = i + 1.0;
                double id = partId / 10;
                var entity = new FilmCopy();
                entity.MovieId = movieId;
                entity.FilmCopyId = movieId + id;
                var filmCopy = _mapper.Map<FilmCopy>(entity);
                _context.Add(filmCopy);
                _context.SaveChangesAsync();
            }
        }

        public void CreateCopies(int oldCopies, int newCopies, int movieId)
        {
            for (double i = oldCopies; i < newCopies; i++)
            {
                double partId = i + 1.0;
                double id = partId / 10;
                var entity = new FilmCopy();
                entity.MovieId = movieId;
                entity.FilmCopyId = movieId + id;
                var filmCopy = _mapper.Map<FilmCopy>(entity);
                _context.Add(filmCopy);
                _context.SaveChangesAsync();
            }
        }

        public void DeleteCopies(int amount, IEnumerable<FilmCopy> filmCopies)
        {
            int currentCount = filmCopies.Count();
            foreach (var filmCopy in filmCopies)
            {
                _context.Remove(filmCopy);
                if (currentCount <= amount)
                {
                    break;
                }
                currentCount--;
                _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<FilmCopy>> GetAllRentedFilmCopiesAsync()
        {
            return await _context.FilmCopies.Where(f => f.IsRented == true).ToListAsync();
        }

        public async Task<IEnumerable<FilmCopy>> GetAllFilmCopiesByMovieIdAsync(int movieId)
        {
            return await _context.FilmCopies.Where(f => f.MovieId == movieId).ToListAsync();
        }

        public async Task<FilmCopy> GetAvaibleFilmCopyByMovieIdAsync(int movieId)
        {
            return await _context.FilmCopies.Where(f => f.MovieId == movieId).FirstOrDefaultAsync(f => f.IsRented == false);
        }
    }
}
