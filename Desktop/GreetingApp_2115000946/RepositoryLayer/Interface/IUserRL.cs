using ModelLayer.Model;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        bool Register(UserModel user);
    

        UserModel GetUserByEmail(string email);
        bool UpdatePassword(string email, string newPassword);
    }
}
