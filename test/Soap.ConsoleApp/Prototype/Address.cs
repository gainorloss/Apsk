using System;

namespace Soap.ConsoleApp
{
    [Serializable]
    public class Address
        :ICloneable
    {
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string DetailAddress { get; set; }
        public string Mobile { get; set; }
        public string Postcode { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}