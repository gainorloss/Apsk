using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Soap.ConsoleApp
{
    [Serializable]
    public class Prototype
         : IPrototype
    {
        public object Clone()
        {
            return MemberwiseClone();
        }

        public object Copy()
        {
            using (var ms = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(ms, this);

                ms.Seek(0, SeekOrigin.Begin);

                return binaryFormatter.Deserialize(ms);
            }
        }
    }
}
