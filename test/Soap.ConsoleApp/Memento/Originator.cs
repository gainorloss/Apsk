using System;

namespace Soap.ConsoleApp
{
    public class Originator
    {
        public string State { get; private set; }

        public Memento CreateMemento()
        {
            return new Memento(State);
        }

        public void RecoverMemento(Memento memento)
        {
            State = memento.State;
        }

        public void SetState(string state)
        {
            State = state;
        }
    }
}
