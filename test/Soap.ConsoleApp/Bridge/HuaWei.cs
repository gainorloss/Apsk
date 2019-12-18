namespace Soap.ConsoleApp.Bridge
{
    public class HuaWei
        : Phone
    {
        public override void Run()
        {
            _address.Run();
            _game.Run();
        }
    }
}
