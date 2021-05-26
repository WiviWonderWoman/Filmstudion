namespace TheMovieStudio.Domain.Models
{
    public class FilmCopy
    {
        public double FilmCopyId { get; set; }
        public int MovieId { get; set; }
        public bool IsRented { get; set; } = false;
        public int FilmStudioId { get; set; }
    }
}
