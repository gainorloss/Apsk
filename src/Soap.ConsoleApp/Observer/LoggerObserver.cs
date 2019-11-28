namespace Soap.ConsoleApp
{
    public class LoggerObserver
        : IObserver
    {
        public void Modify() => System.Console.WriteLine("logging...");
    }
}
