using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;
using System.Linq;

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

        public UserModel GetUserByEmail(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);
        }
    }
}
