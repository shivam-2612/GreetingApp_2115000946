using RepositoryLayer.Interface;
using ModelLayer.Model;
using System.Linq;
using RepositoryLayer.Context;

namespace RepositoryLayer.Service
{
    public class GreetingRL : IGreetingRL
    {
        private readonly GreetingDbContext _dbContext;

        public GreetingRL(GreetingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string GetGreeting(string firstName = null, string lastName = null)
        {
            GreetingModel greetingModel = new GreetingModel
            {
                FirstName = firstName,
                LastName = lastName
            };

            string message = greetingModel.GenerateGreeting();

            // Save greeting in database
            SaveGreeting(new GreetingEntity
            {
                FirstName = firstName,
                LastName = lastName,
                Message = message,
                CreatedAt = DateTime.Now
            });

            return message;
        }

        public void SaveGreeting(GreetingEntity greeting)
        {
            _dbContext.Greetings.Add(greeting);
            _dbContext.SaveChanges();
        }


        public GreetingEntity GetGreetingById(int id)  // New Method
        {
            return _dbContext.Greetings.FirstOrDefault(g => g.Id == id);
        }



        public List<GreetingEntity> GetAllGreetings()  // New Method
        {
            return _dbContext.Greetings.ToList();
        }


        public GreetingEntity UpdateGreeting(GreetingEntity updatedGreeting)  // New Method
        {
            var existingGreeting = _dbContext.Greetings.FirstOrDefault(g => g.Id == updatedGreeting.Id);

            if (existingGreeting != null)
            {
                existingGreeting.FirstName = updatedGreeting.FirstName ?? existingGreeting.FirstName;
                existingGreeting.LastName = updatedGreeting.LastName ?? existingGreeting.LastName;
                existingGreeting.Message = updatedGreeting.Message ?? existingGreeting.Message;
                existingGreeting.CreatedAt = updatedGreeting.CreatedAt != default ? updatedGreeting.CreatedAt : existingGreeting.CreatedAt;

                _dbContext.SaveChanges();
                return existingGreeting;
            }
            return null;
        }


        public bool DeleteGreeting(int id)
        {
            var greeting = _dbContext.Greetings.FirstOrDefault(g => g.Id == id);
            if (greeting != null)
            {
                _dbContext.Greetings.Remove(greeting);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
