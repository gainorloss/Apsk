namespace Soap.ConsoleApp
{
    public class StrategyContext
    {
        private readonly ILogger _logger;
        public StrategyContext(ILogger logger)
        {
            _logger = logger;
        }

        public void Write()
        {
            _logger.Write();
        }
    }
}
