using System.Collections.Generic;

namespace Infrastructure.Data.Abstractions
{
    public class Sort
    {
        public ICollection<Order> Orders { get; private set; }
        public Sort(params Order[] orders)
        {
            Orders = orders;
        }

        public Sort(IList<Order> orders)
        {
            Orders = orders;
        }
        public Sort(Direction direction, params string[] fields)
        {
            Orders = new List<Order>();

            foreach (var field in fields)
                Orders.Add(new Order(direction, field));
        }
    }
}