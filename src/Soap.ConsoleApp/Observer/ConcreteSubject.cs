using System.Collections.Generic;
using System.Linq;

namespace Soap.ConsoleApp
{
    public class ConcreteSubject
        : ISubject
    {
        private List<IObserver> _observers;

        public IReadOnlyCollection<IObserver> Observers => _observers ?? new List<IObserver>();
        public string SubjectState { get; set; }

        public void AddObserver(IObserver observer)
        {
            if (_observers == null)
                _observers = new List<IObserver>();

            _observers.Add(observer);
        }

        public void Notify()
        {
            _observers.ForEach(o => o.Modify());
        }

        public void RemoveObserver(IObserver observer)
        {
            if (_observers == null || !_observers.Any())
                return;
            _observers.Remove(observer);
        }
    }
}
