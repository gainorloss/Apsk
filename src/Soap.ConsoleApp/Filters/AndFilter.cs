using Soap.ConsoleApp.Models;
using System.Collections.Generic;

namespace Soap.ConsoleApp
{
    public class AndFilter
        : IFilter
    {
        public List<IFilter> Filters = new List<IFilter>();
        public AndFilter(List<IFilter> filters)
        {
            Filters = filters;
        }
        public IEnumerable<Student> Filter(IEnumerable<Student> persons)
        {
            foreach (var filter in Filters)
                persons = filter.Filter(persons);

            return persons;
        }
    }
}
