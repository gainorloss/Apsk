using System;

namespace Apsk.Utils.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class XlsSheetAttribute
        : Attribute
    {
        public XlsSheetAttribute(string sheetName)
        {
            SheetName = sheetName;
        }
        public XlsSheetAttribute(Type type)
        {
            Name = type.Name;
            SheetName = type.Name;
            FullName = type.FullName;
        }
        public string SheetName { get; set; }
        public string Name { get; private set; }
        public string FullName { get; private set; }
    }
}
