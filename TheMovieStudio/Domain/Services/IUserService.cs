using System.Collections.Generic;
using TheMovieStudio.Domain.Models;

namespace TheMovieStudio.Domain.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Create(User user, string password);
    }
}
