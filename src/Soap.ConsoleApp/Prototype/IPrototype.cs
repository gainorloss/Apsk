using System;

namespace Soap.ConsoleApp
{
    public interface IPrototype:ICloneable
    {
        object Copy();
    }
}
