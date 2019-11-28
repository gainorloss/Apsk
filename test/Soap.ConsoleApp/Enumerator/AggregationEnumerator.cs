using System.Collections;
using System.Collections.Generic;

namespace Soap.ConsoleApp
{
    public class AggregationEnumerator
        : IEnumerator<int>
    {
        private Aggregation _aggregation;

        private AggregationEnumerator()
        { }
        public AggregationEnumerator(Aggregation aggregation)
        {
            _aggregation = aggregation;
            _current = _aggregation[_idx];
        }

        private int _idx = 0;
        private int _current;
        public int Current => _current;

        object IEnumerator.Current => _current;

        public void Dispose()
        {
            _aggregation = null;
        }

        public bool MoveNext()
        {
            if (_idx < _aggregation.Count)
            {
                _current = _aggregation[_idx];
                _idx++;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _idx = 0;
        }
    }
}
