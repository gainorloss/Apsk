namespace Soap.ConsoleApp.Bridge
{
    public abstract class Phone
    {
        protected ContactBook _address;
        protected Game _game;
        public virtual void SetContactBook(ContactBook address)
        {
            _address = address;
        }
        public virtual void SetGame(Game game)
        {
            _game = game;
        }

        public abstract void Run();
    }
}
