using System.ComponentModel.DataAnnotations;

namespace TheMovieStudio.Resources.Users
{
    public class RegisterFilmStudioModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string ChairpersonName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int Id { get; set; }
        public bool IsAdmin = false;
    }
}
