using ModelLayer.Model;
using RepositoryLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IGreetingBL
    {
        string GetGreetingMessage(string firstName = null, string lastName = null);
        GreetingEntity GetGreetingById(int id);  // New method

    }
}
