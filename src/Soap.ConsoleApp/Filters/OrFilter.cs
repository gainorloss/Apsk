using System.Collections.Generic;
using Soap.ConsoleApp.Models;

namespace Soap.ConsoleApp
{
    public class OrFilter
        : IFilter
    {
        public List<IFilter> Filters = new List<IFilter>();
        public OrFilter(List<IFilter> filters)
        {
            Filters = filters;
        }
        public IEnumerable<Student> Filter(IEnumerable<Student> students)
        {
            var hashSet = new HashSet<Student>();

            IEnumerable<Student> items = null;
            foreach (var filter in Filters)
            {
                items = filter.Filter(students);

                foreach (var item in items)
                    hashSet.Add(item);
            }

            return hashSet;
        }
    }
}
