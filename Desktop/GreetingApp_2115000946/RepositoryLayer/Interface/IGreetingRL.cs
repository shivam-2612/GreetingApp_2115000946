﻿using System;
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
        bool DeleteGreeting(int id);
        List<GreetingEntity> GetAllGreetings();
        string GetGreeting(string firstName = null, string lastName = null);
        GreetingEntity GetGreetingById(int id);
        void SaveGreeting(GreetingEntity greeting);  // New method to save greetings
        GreetingEntity UpdateGreeting(GreetingEntity updatedGreeting);


    }

}
