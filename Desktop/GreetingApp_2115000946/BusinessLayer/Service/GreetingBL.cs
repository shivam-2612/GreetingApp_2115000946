using System;
using BusinessLayer.Interface;
using ModelLayer.Model;
using RepositoryLayer.Interface;

namespace BusinessLayer.Service
{
    public class GreetingBL : IGreetingBL
    {
        private readonly IGreetingRL _greetingRL;

        public GreetingBL(IGreetingRL greetingRL)
        {
            _greetingRL = greetingRL;
        }

        public string GetGreetingMessage(string firstName = null, string lastName = null)
        {
            GreetingModel greetingModel = new GreetingModel
            {
                FirstName = firstName,
                LastName = lastName
            };

            string message = greetingModel.GenerateGreeting();

            // Save greeting in repository
            GreetingEntity greetingEntity = new GreetingEntity
            {
                FirstName = firstName,
                LastName = lastName,
                Message = message
            };

            _greetingRL.SaveGreeting(greetingEntity);

            return message;
        }


        public GreetingEntity GetGreetingById(int id)  // New Method
        {
            return _greetingRL.GetGreetingById(id);
        }



        public List<GreetingEntity> GetAllGreetings()  // New Method
        {
            return _greetingRL.GetAllGreetings();
        }
    }
}
