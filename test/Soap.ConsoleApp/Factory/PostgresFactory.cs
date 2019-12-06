namespace Soap.ConsoleApp
{
    public class PostgresFactory : IFactory
    {
        public IOrder CreateOrder()
        {
            return new PostgresOrder();
        }

        public IUser CreateUser()
        {
            return new PostgresUser();
        }
    }
}
