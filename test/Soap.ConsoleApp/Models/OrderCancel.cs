using System.Collections.Generic;

namespace Soap.ConsoleApp.Models
{

    public class orderCancel
    {
        public orderCancel()
        {
            items = new List<orderCancelSku>();
        }
        public string externorderkey { get; set; }
        public string storerkey { get; set; }
        public string whid { get; set; }
        public List<orderCancelSku> items { get; set; }
    }
}