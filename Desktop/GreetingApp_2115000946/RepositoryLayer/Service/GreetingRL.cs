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
    }
}
