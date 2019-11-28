using Soap.ConsoleApp.Models;
using System.Collections;
using System.Collections.Generic;

namespace Soap.ConsoleApp
{
    public interface IFilter
    {
        IEnumerable<Student> Filter(IEnumerable<Student> persons);
    }
}
