using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.Model;
using RepositoryLayer.DTO;

namespace RepositoryLayer.Interface
{
   
    public interface IGreetingRL
    {
        string GetGreeting(string firstName = null, string lastName = null);
        void SaveGreeting(GreetingEntity greeting);  // New method to save greetings
    }

}
