using System.Xml.Serialization;

namespace Soap.ConsoleApp.Models
{
    public class OrderModify
    {
        [XmlElement("address")]
        public string Address { get; set; }
        [XmlElement("carrier")]
        public string Carrier { get; set; }

        [XmlElement("carriername")]
        public string Carriername { get; set; }

        [XmlElement("custname")]
        public string Custname { get; set; }
        [XmlElement("expressdtb")]
        public string Expressdtb { get; set; }
        [XmlElement("expressjhd")]
        public string Expressjhd { get; set; }
        [XmlElement("expressnumber")]
        public string Epressnumber { get; set; }

        [XmlElement("externorderkey")]
        public string Externorderkey { get; set; }
        [XmlElement("expresstype")]
        public string Expresstype { get; set; }
        [XmlElement("phone")]
        public string Phone { get; set; }
        [XmlElement("phone2")]
        public string Phone2 { get; set; }
        [XmlElement("storerkey")]
        public string Storerkey { get; set; }
        [XmlElement("whid")]
        public string Whid { get; set; }
        
    }
}