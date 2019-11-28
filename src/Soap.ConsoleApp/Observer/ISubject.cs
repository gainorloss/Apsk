namespace Soap.ConsoleApp
{
    public interface ISubject
    {
        string SubjectState { get; set; }

        void AddObserver(IObserver observer);

        void RemoveObserver(IObserver observer);

        void Notify();
    }
}
