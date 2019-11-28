namespace Infrastructure.Data.Abstractions
{
    public class Order
    {
        private Order()
        { }
        public Order(Direction direction, string field)
        {
            Direction = direction;
            Field = field;
        }
        public string Field { get; private set; }
        public Direction Direction { get; private set; }
    }
}