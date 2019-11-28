namespace Soap.ConsoleApp
{
    public class ZhangSan
        : AbstractExam
    {
        public ZhangSan()
        {
            Name = "Zhang San";
        }
        protected override string Answer()
        {
            return "A";
        }
    }
}
