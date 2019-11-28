using System.Collections.Generic;

namespace Soap.ConsoleApp
{
    public class Aggregation
    {
        private readonly List<int> _items = new List<int>();


        public int Count => _items.Count;
        public int this[int idx] => _items[idx];

        public AggregationEnumerator GetEnumerator() => new AggregationEnumerator(this);
        public void Add(int item)
        {
            _items.Add(item);
        }
    }
}
