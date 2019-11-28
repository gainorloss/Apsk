using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Soap.ConsoleApp.Models
{
    [Serializable]
    [XmlRoot("impOrderModify", Namespace = "http://com.test/OrderModify")]
    public class ImpOrderModifyRequest
    {
        public ImpOrderModifyRequest()
        {
            List = new List<OrderModify>();
        }
        [XmlArray("list")]
        [XmlArrayItem(typeof(OrderModify))]
        public List<OrderModify> List { get; set; }
    }
}
