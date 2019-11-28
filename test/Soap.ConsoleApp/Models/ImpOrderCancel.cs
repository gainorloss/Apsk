using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Soap.ConsoleApp.Models
{
    [Serializable]
    [XmlRoot("impOrderCancel", Namespace = "http://com.test/OrderCancel")]
    public class ImpOrderCancel
    {
        public ImpOrderCancel()
        {
            list = new List<orderCancel>();
        }
        public List<orderCancel> list { get; set; }
    }
}
