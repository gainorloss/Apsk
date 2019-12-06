using System;

namespace BootSharp.API.Models
{
    public class User
    {
        public User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Birthday = DateTime.Now;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; private set; }
        public string Introduction { get; private set; }
    }
}
