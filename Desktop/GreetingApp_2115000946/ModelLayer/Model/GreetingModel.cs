using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ModelLayer.Model
{
    public class GreetingModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }

        public string GenerateGreeting()
        {
            if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName))
                return $"Hello, {FirstName} {LastName}!";
            else if (!string.IsNullOrEmpty(FirstName))
                return $"Hello, {FirstName}!";
            else if (!string.IsNullOrEmpty(LastName))
                return $"Hello, {LastName}!";
            else
                return "Hello, World!";
        }
    }
}

