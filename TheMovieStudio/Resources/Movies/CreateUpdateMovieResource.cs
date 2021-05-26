using System.ComponentModel.DataAnnotations;

namespace TheMovieStudio.Resources.Movies
{
    public class CreateUpdateMovieResource
    {
        public int MovieId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Range(1900, 2021)]
        public int ReleaseYear { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        [Range(1, 9)]
        public int AmountOfCopies { get; set; }
    }
}
