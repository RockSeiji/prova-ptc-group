using Cast.Models;

namespace Cast.services.Interface
{
    public interface IAuthService
    {
        void Register(User user);
        string Login(Login login);
    }
}
