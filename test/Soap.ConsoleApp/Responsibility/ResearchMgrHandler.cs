namespace Soap.ConsoleApp
{
    public class ResearchMgrHandler
        : AbstractHandler
    {
        public override void Handle(int state)
        {
            if (state < 5 && state >= 3)
            {
                System.Console.WriteLine("研发经理可以决定");
            }
            else
            {
                if (_handler!=null)
                    _handler.Handle(state);
            }
        }
    }
}
