namespace Soap.ConsoleApp
{
    public abstract class AbstractExam
    {
        protected string Name { get; set; }
        public void Question()
        {
            System.Console.WriteLine($"{Name} 今天，暖和吗？{Answer()}");
        }
        protected abstract string Answer();
    }
}
