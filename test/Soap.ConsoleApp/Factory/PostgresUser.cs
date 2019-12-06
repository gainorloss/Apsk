namespace Soap.ConsoleApp
{
    public class PostgresUser : IUser
    {
        public void Add(long id)
        {
            System.Console.WriteLine("postgres user...");
        }
    }
}
