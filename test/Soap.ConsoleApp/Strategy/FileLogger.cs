namespace Soap.ConsoleApp
{
    public class FileLogger
        : ILogger
    {
        public void Write()
        {
            System.Console.WriteLine($"【$FileLogger】:(无线电)");
        }
    }
}
