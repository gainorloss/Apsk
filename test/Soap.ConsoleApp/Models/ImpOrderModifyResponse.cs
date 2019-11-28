using System.Collections.Generic;
using System.Xml.Serialization;

namespace Soap.ConsoleApp.Models
{
    [XmlRoot("impOrderModifyResponse",Namespace = "http://com.test/OrderModify")]
    public class ImpOrderModifyResponse
    {
        [XmlArray("out")]
        [XmlArrayItem(typeof(ReturnMessage),Namespace = "http://business.wms.job.wtit.com")]
        public List<ReturnMessage> Out { get; set; }
    }
}
