using AutoMapper;
using TheMovieStudio.Domain.Models;
using TheMovieStudio.Resources;
using TheMovieStudio.Resources.Movies;
using TheMovieStudio.Resources.Public;

namespace TheMovieStudio.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<FilmStudio, PublicFilmStudioResource>();

            CreateMap<Movie, MovieResource>();
            CreateMap<Movie, CreateUpdateMovieResource>();
            CreateMap<Movie, PublicMovieResource>();

            CreateMap<FilmCopy, FilmCopyResource>();
        }
    }
}
