namespace Szkolenie_Kamsoft.Patterns
{
    public interface ICommandHandler<in T> 
    {
        public Task Handle(T command);
    }
    public interface IQueryHandler<in TQuery, TResult>
    {
        public Task<TResult> Handle(TQuery query);
    }
    public record CommandInput(Guid Id);
    public class ExampleCommandHandler : ICommandHandler<CommandInput>
    {
        public Task Handle(CommandInput command)
        {
            return Task.CompletedTask;
        }
    }
}
