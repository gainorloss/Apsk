using Soap.ConsoleApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace Soap.ConsoleApp
{
    public class AgeFilter
          : IFilter
    {
        public IEnumerable<Student> Filter(IEnumerable<Student> persons)
        {
            return persons.Where(p=>p.Age>=25);
        }
    }
}
