using System.Linq;

namespace Soap.ConsoleApp
{
    public class ConcreteMediator
           : AbstractMediator
    {
        public override void Send(AbstractUser user)
        {
            var usr = _users.FirstOrDefault(u => u == user);
            if (usr!=null)
            {
                usr.Receive();
            }
        }
    }
}
