using System;
using System.Threading.Tasks;
using Mapster;

namespace LawnKeeper.ConsoleTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("bonjour\n");

            var model = new UserViewModel()
            {
                Email = "test@e.mail",
                Name = "Nick",
            };
            var user = new User()
            {
                Password = "qwe123",
            };
            model.Adapt(user);
            
            Console.WriteLine(model);
            Console.WriteLine(user);
        }
    }

    class User
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"{Email}, {Name}, {Password}";
        }
    }

    class UserViewModel
    {
        public string Email { get; set; }
        public string Name { get; set; }
        
        public override string ToString()
        {
            return $"{Email}, {Name}";
        }
    }
}