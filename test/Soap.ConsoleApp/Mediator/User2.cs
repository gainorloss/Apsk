namespace Soap.ConsoleApp
{
    public class User2
         : AbstractUser
    {
        public User2(AbstractMediator mediator) : base(mediator)
        {
        }

        public override void Receive()
        {
            System.Console.WriteLine("user2...."); 
        }
    }
}
