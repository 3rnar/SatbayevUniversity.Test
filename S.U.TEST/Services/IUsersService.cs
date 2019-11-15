using System.Collections.Generic;
using S.U.TEST.Models;
using S.U.TEST.Models.ViewModels;

namespace S.U.TEST.Services
{
    public interface IUsersService
    {
        string AuthUser(Auth authModel);
        IEnumerable<User> GetAllUsers();
        User GetUserByID(int id);
        void RegistrationOfNewUser(Registration user);
    }
}