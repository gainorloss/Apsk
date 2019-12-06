using System;

namespace Soap.ConsoleApp
{
    public class User
    {
        //public string Name { get; set; }

        public static readonly string Token;

        public string Id { get; set; }
        public string Name { get; set; }
        static User()
        {
            Token = "ppm.erp";
        }

        public User()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        //public void SetPassword(string password)
        //{
        //    Password = password;
        //}
    }
}
