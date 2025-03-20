using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System.Linq;
using BCrypt.Net;


namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly GreetingDbContext _context;

        public UserRL(GreetingDbContext context)
        {
            _context = context;
        }

        public bool Register(UserModel user)
        {
            _context.Users.Add(user);
            return _context.SaveChanges() > 0;
        }
        public UserModel GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }



        public bool UpdatePassword(string email, string newPassword)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return false;

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            _context.SaveChanges();
            return true;
        }
    }
}
