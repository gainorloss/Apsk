namespace Soap.ConsoleApp
{
    public class PostgresOrder : IOrder
    {
        public void Add()
        {
            System.Console.WriteLine("postgres order...");
        }
    }
}