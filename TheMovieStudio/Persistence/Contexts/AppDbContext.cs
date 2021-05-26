using Microsoft.EntityFrameworkCore;
using TheMovieStudio.Domain.Models;

namespace TheMovieStudio.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<FilmStudio> FilmStudios { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<FilmCopy> FilmCopies { get; set; }
        public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //---------------FILMSTUDIOS-----------------------//
            builder.Entity<FilmStudio>().HasData(new FilmStudio
            {
                FilmStudioId = 1,
                Name = "Röda Kvarn",
                Location = "Helsingborg",
                ChairpersonName = "Kim Olsson",
                Email = "kim.olsson@rodakvarn.se"
            });

            builder.Entity<FilmStudio>().HasData(new FilmStudio
            {
                FilmStudioId = 2,
                Name = "Sagabiografen",
                Location = "Höganäs",
                ChairpersonName = "Charlie Nilsson",
                Email = "charlie.nilsson@sagabiografen.se"
            });

            //---------------USERS-----------------------//
            builder.Entity<User>().HasData(new User
            {
                FilmStudioId = 1,
                Email = "kim.olsson@rodakvarn.se",
                IsAdmin = false,
                Id = 1,
                Password = "P@ssw0rd!1"
            });

            builder.Entity<User>().HasData(new User
            {
                FilmStudioId = 2,
                Email = "charlie.nilsson@sagabiografen.se",
                IsAdmin = false,
                Id = 2,
                Password = "P@ssw0rd!2"
            });

            //---------------MOVIES-------------------//
            builder.Entity<Movie>().HasData(new Movie
            {
                MovieId = 1,
                Title = "La jetée (Terassen)",
                Director = "Chris Marker",
                Country = "Frankrike",
                ReleaseYear = 1962,
                AmountOfCopies = 2
            });

            builder.Entity<Movie>().HasData(new Movie
            {
                MovieId = 2,
                Title = "For Sama (Till min dotter)",
                Director = "Waad Al-Kateab, Edward Watts",
                Country = "Storbritannien",
                ReleaseYear = 2019,
                AmountOfCopies = 4
            });

            builder.Entity<Movie>().HasData(new Movie
            {
                MovieId = 3,
                Title = "花樣年華 (In the Mood for Love)",
                Director = "Wong Kar-Wai",
                Country = "Kina",
                ReleaseYear = 2000,
                AmountOfCopies = 3
            });

            //---------------FILMCOPIES---------------------//
            builder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 1.1,
                MovieId = 1,
                IsRented = true,
                FilmStudioId = 1
            });

            builder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 1.2,
                MovieId = 1,
                IsRented = false,
                FilmStudioId = 0
            });

            builder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 2.1,
                MovieId = 2,
                IsRented = false,
                FilmStudioId = 0
            });
            builder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 2.2,
                MovieId = 2,
                IsRented = false,
                FilmStudioId = 0
            });
            builder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 2.3,
                MovieId = 2,
                IsRented = true,
                FilmStudioId = 3
            });
            builder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 2.4,
                MovieId = 2,
                IsRented = false,
                FilmStudioId = 0
            });

            builder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 3.1,
                MovieId = 3,
                IsRented = false,
                FilmStudioId = 0
            });
            builder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 3.2,
                MovieId = 3,
                IsRented = true,
                FilmStudioId = 2
            });
            builder.Entity<FilmCopy>().HasData(new FilmCopy
            {
                FilmCopyId = 3.3,
                MovieId = 3,
                IsRented = true,
                FilmStudioId = 1
            });
        }
    }
}
