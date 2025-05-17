namespace Szkolenie_Kamsoft.Patterns
{
    public interface IHandle<T>
    {
        public Task Handle(T input);
    }
    public class HandleOne : IHandle<string> 
    {
        public Task Handle(string input)
        {
            Console.WriteLine(input);
            return Task.CompletedTask;
        }
    }
    public class HandleTwo : IHandle<string>
    {
        private readonly IHandle<string> _handle;
        public HandleTwo(IHandle<string> handle)
        {
            _handle = handle;
        }
        public async Task Handle(string input)
        {
            Console.WriteLine(input);
            await _handle.Handle(input);
        }
    }
    // handleThree..
}
