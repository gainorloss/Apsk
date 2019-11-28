using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Soap.ConsoleApp.Models
{
    [XmlRoot("impOrderCancelResponse",Namespace = "http://com.test/OrderCancel")]
    public class ImpOrderCancelResponse
    {
        [XmlArray("out")]
        [XmlArrayItem(typeof(ReturnMessage),Namespace = "http://business.wms.job.wtit.com")]
        public List<ReturnMessage> Out { get; set; }
    }
}
