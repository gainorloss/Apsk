namespace Soap.ConsoleApp
{
    public class LeaderHandler
        : AbstractHandler
    {
        public override void Handle(int state)
        {
            if (state >= 5)
                System.Console.WriteLine("只能部门老大处理了");
        }
    }
}
