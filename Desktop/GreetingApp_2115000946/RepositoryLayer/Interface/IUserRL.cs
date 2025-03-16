using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        bool Register(UserModel user);
        string Login(string email, string password);
    }
}

