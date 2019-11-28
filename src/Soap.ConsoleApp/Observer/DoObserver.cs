namespace Soap.ConsoleApp
{
    public class DoObserver
          : IObserver
    {
        public void Modify() => System.Console.WriteLine("do...");
    }
}
