namespace Soap.ConsoleApp
{
    public class DatabaseLogger
        : ILogger
    {
        public void Write()
        {
            System.Console.WriteLine($"【$DatabaseLogger】:(无线电)");
        }
    }
}
