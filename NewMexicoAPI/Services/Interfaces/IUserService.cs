using NewMexicoAPI.Models;

namespace NewMexicoAPI.Services.Interfaces
{
    public interface IUserService
    {
        bool IsValidUserInformation(LoginModel model);
        List<LoginModel> GetUserDetails();
        bool AddUserDetails(string username, string password);
    }
}
