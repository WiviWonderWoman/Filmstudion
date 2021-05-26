using AutoMapper;
using TheMovieStudio.Domain.Models;
using TheMovieStudio.Resources;
using TheMovieStudio.Resources.Movies;
using TheMovieStudio.Resources.Users;

namespace TheMovieStudio.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<RegisterAdminUserModel, User>();

            CreateMap<RegisterFilmStudioModel, FilmStudio>();
            CreateMap<RegisterFilmStudioModel, User>();

            CreateMap<CreateUpdateMovieResource, Movie>();

            CreateMap<FilmCopyResource, FilmCopy>();
        }
    }
}
