using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ModelLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Interface;


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

        public string Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == password);
            return user != null ? "Login Successful" : "Invalid Credentials";
        }
    }
}
