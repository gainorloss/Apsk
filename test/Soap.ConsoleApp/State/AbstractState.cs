namespace Soap.ConsoleApp
{
    public abstract class AbstractState
    {
        public int Hour { get; set; }
        public abstract void Handle(StateContext ctx);
    }
}
