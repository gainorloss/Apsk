namespace Soap.ConsoleApp
{
    public interface IFactory
    {
        IUser CreateUser();

        IOrder CreateOrder();
    }
}
