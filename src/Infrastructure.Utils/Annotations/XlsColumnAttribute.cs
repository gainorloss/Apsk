using System;
using System.Reflection;

namespace Infrastructure.Utils.Annotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class XlsColumnAttribute
       : Attribute
    {
        public XlsColumnAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string Prompt { get; set; }
        public string Format { get; set; }
        public bool IsBold { get; set; }
        public PropertyInfo Property { get; set; }
        public short Forground { get; set; } = 255;
        public int Order { get; set; }
    }
}
