using System.Xml.Serialization;

namespace Soap.ConsoleApp.Models
{
    [XmlRoot("getFLZLByZyIDResponse")]
    public class getFLZLByZyIDResponse
    {
        [XmlArray("out")]
        [XmlArrayItem(typeof(byte))]
        public byte[] Out { get; set; }
    }
}
