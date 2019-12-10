namespace Soap.ConsoleApp
{
    public abstract class AbstractHandler
    {
        protected AbstractHandler _handler;
        public abstract void Handle(int state);
        public virtual void SetNext(AbstractHandler handler = null) => _handler = handler;
    }
}
