namespace Soap.ConsoleApp.Decorator
{
    public class HouseDecorator
        : House
    {
        private House _house;
        public HouseDecorator(House house)
        {
            _house = house;
        }
        public override void Build()
        {
            System.Console.WriteLine("build start...");
            _house.Build();
            System.Console.WriteLine("build end...");
        }
    }
}
