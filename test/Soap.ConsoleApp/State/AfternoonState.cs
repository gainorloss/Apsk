namespace Soap.ConsoleApp
{
    public class AfternoonState
        :AbstractState
    {
        public override void Handle(StateContext ctx)
        {
            if (Hour>12&&Hour<=2)
            {
                System.Console.WriteLine("中午好");
            }
        }
    }
}
