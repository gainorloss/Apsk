namespace Soap.ConsoleApp
{
    public class DevelopMgrHandler
        : AbstractHandler
    {
        public override void Handle(int state)
        {
            if (state < 3)
                System.Console.WriteLine("开发经理可以决定。。。");
            else
            {
                if (_handler != null)
                    _handler.Handle(state);
            }
        }
    }
}
