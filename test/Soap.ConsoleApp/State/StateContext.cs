namespace Soap.ConsoleApp
{
    public class StateContext
    {
        public AbstractState State { get; set; }
        public void Request()
        {
            State.Handle(this);
        }
    }
}
