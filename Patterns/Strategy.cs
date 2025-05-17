namespace Szkolenie_Kamsoft.Patterns
{
    public interface IStrategy
    {
        bool IsValid();
    }
    public class StrategyPersonSimple : IStrategy
    {
        public bool IsValid()
        {
            return true;
        }
    }
    public class StrategyWithPesel : IStrategy
    {
        public bool IsValid()
        {
            return true;
        }
    }
}
