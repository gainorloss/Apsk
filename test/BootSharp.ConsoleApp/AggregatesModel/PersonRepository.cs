using Infrastructure.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootSharp.ConsoleApp.AggregatesModel
{
    [Component]
    public class PersonRepository
        : IPersonRepository
    {
        private static readonly IList<Person> persons = new List<Person>();
        public Task CreatePersonAsync(Person person)
        {
            persons.Add(person);
            return Task.CompletedTask;
        }
    }
}
