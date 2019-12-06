namespace Soap.ConsoleApp
{
    public class MorningState
        :AbstractState
    {
        public override void Handle(StateContext ctx)
        {
            if (Hour>=0&&Hour<12)
            {
                System.Console.WriteLine("早上好");
            }
            else
            {
                ctx.State = new AfternoonState();
            }
        }
    }
}
