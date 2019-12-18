namespace Soap.ConsoleApp.Decorator
{
    public class ConcreteHouse
        : House
    {
        public override void Build()
        {
            System.Console.WriteLine("Build ...");
        }
    }
}
