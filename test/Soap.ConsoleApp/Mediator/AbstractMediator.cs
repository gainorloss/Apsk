using System.Collections.Generic;

namespace Soap.ConsoleApp
{
    /// <summary>
    /// 中介者抽象
    /// </summary>
    public abstract class AbstractMediator
    {
        protected List<AbstractUser> _users = new List<AbstractUser>();
        public abstract void Send(AbstractUser user);

        public void Add(AbstractUser user)
        {
            _users.Add(user);
        }
    }
}
