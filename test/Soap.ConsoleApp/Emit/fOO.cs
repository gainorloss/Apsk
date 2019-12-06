namespace Soap.ConsoleApp
{
    public class Foo<T> : Bar, IFoo<T>
         where T : struct
    {
        private T _name;
        public Foo(T name)
            : base()
        {
            _name = name;
        }
        public T Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public override void DisplayName()
        {
            System.Console.WriteLine(Name.ToString());
        }
    }
}
