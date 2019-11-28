using System;
using System.Xml.Serialization;

namespace Soap.ConsoleApp.Models
{
    [Serializable]
    public class Hello
    {
        public string Name { get; set; }
    }
}
