namespace Szkolenie_Kamsoft.Patterns
{
    public sealed class Singleton
    {
        private static Singleton _instance;
        // Private constructor to prevent instantiation
        private Singleton()
        {
        }
        // Public method to get the instance of the class
        public static Singleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }
        // Example method
        public void DoSomething()
        {
            Console.WriteLine("Doing something...");
        }
    }
}
