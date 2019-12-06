namespace Soap.ConsoleApp
{
    public class User1
           : AbstractUser
    {
        public User1(AbstractMediator mediator) : base(mediator)
        {
        }

        public override void Receive()
        {
            System.Console.WriteLine("user1....");
        }
    }
}
