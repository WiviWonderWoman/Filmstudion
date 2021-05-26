using System.Collections.Generic;
using TheMovieStudio.Domain.Models;

namespace TheMovieStudio.Resources.Movies
{
    public class MovieResource
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Country { get; set; }
        public string Director { get; set; }
        public int AvailablefCopies { get; set; }
        public IEnumerable<FilmCopy> AvailableFilmcopies { get; set; }
    }
}
