using System.Collections.Generic;
using System.Xml.Serialization;

namespace Soap.ConsoleApp.Models
{
    [XmlRoot("getSupportProvinceResponse",Namespace = "http://WebXml.com.cn/")]
    public class GetSupportProvinceResponse
    {
        [XmlArray("getSupportProvinceResult")]
        [XmlArrayItem(typeof(string))]
        public List<string> GetSupportProvinceResult { get; set; }
    }
}
