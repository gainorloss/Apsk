namespace Soap.ConsoleApp
{
    public abstract class AbstractUser
    {
        protected AbstractMediator _mediator;
        public AbstractUser(AbstractMediator mediator)
        {
            _mediator = mediator;
        }
        public virtual void Send(AbstractUser usr)
        {
            _mediator.Send(usr);
        }
        public abstract void Receive();
    }
}
