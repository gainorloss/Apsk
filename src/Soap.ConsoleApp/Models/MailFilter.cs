using System.Collections.Generic;
using System.Linq;

namespace Soap.ConsoleApp.Models
{
    public class MailFilter
          : IFilter
    {
        public IEnumerable<Student> Filter(IEnumerable<Student> persons)
        {
            return persons.Where(p=>p.Email.Equals("519564415@qq.com"));
        }
    }
}
