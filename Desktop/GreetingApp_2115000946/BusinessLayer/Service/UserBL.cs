using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Interface;
using System.Security.Cryptography;


namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL _userRL;

        public UserBL(IUserRL userRL)
        {
            _userRL = userRL;
        }

        public bool Register(UserModel user)
        {
            // Hash Password before saving to DB
            user.PasswordHash = HashPassword(user.PasswordHash);
            return _userRL.Register(user);
        }

        public string Login(string email, string password)
        {
            string hashedPassword = HashPassword(password);
            return _userRL.Login(email, hashedPassword);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}

