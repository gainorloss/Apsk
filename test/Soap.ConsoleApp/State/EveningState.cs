namespace Soap.ConsoleApp
{
    public class EveningState
          : AbstractState
    {
        public override void Handle(StateContext ctx)
        {
            if (Hour>=12&&Hour<24)
            {
                System.Console.WriteLine("晚上好");
            }
            else
            {
                ctx.State = new MorningState();
            }
        }
    }
}
