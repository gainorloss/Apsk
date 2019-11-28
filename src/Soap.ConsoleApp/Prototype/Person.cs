using System;

namespace Soap.ConsoleApp
{
    [Serializable]
    public class Person
        : Prototype,ICloneable,IPrototype
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        //public object Clone()
        //{
        //    return MemberwiseClone();
        //}

        //public object Copy()
        //{
        //    using (var ms=new MemoryStream())
        //    {
        //        BinaryFormatter binaryFormatter = new BinaryFormatter();
        //        binaryFormatter.Serialize(ms,this);

        //        ms.Seek(0, SeekOrigin.Begin);

        //        return binaryFormatter.Deserialize(ms);
        //    }
        //}
    }
}
