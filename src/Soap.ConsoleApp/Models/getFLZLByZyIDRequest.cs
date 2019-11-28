using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Soap.ConsoleApp.Models
{
    [Serializable]
    [XmlRoot("getFLZLByZyID",Namespace = "http://wwdzswj.webservice.com")]
    public class getFLZLByZyIDRequest
    {
        public getFLZLByZyIDRequest()
        {
            In = new List<string>();
        }
        [XmlArray("in0")]
        [XmlArrayItem(typeof(string))]
        public List<string> In { get; set; }
    }
}
