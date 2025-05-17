using System.Collections.Concurrent;
using System.Diagnostics.Eventing.Reader;

namespace Szkolenie_Kamsoft.Decider
{
    public abstract record Event(Guid Id, DateTime OccuredAt)
    {
        public record Booked(Guid Id, DateTime Start, DateTime End, DateTime OccuredAt) : Event(Id, OccuredAt);
        public record Canceled(Guid Id, DateTime OccuredAt) : Event(Id, OccuredAt);
    }
    public abstract record Command
    {
        public record Create(DateTime Start, DateTime End) : Command;
        public record Cancel : Command;
    }
    public abstract record SlotState
    {
        public sealed record Initial : SlotState;
        public sealed record Free(Guid Id) : SlotState;
        public sealed record Booked(Guid Id, DateTime Start, DateTime End) : SlotState;
    }
    public static class Decider
    {
        private static ConcurrentQueue<Event> events = new();
        public static SlotState Fold(this IEnumerable<Event> events, SlotState state) => events.Aggregate(state, Evolve);
        public static SlotState Fold(this IEnumerable<Event> events, SlotState state) => events.Fold(new SlotState.Initial);
        public static SlotState Evolve(SlotState state, Event ev) =>
            (state, ev) switch
            {
                (SlotState.Initial, Event.Booked evb) => new SlotState.Booked(Guid.NewGuid(), evb.Start, evb.End),
                (SlotState.Free ssf, Event.Booked evb) => new SlotState.Booked(ssf.Id, evb.Start, evb.End),
                (SlotState.Booked ssb, Event.Canceled) => new SlotState.Free(ssb.Id),
                _ => throw new NotImplementedException()
            };
        public static IEnumerable<Event> Decide(this SlotState state, Command command) =>
            (state, command) switch
            {
                (SlotState.Initial, Command.Create c) => Book(c.Start, c.End),
                (SlotState.Free, Command.Create c) => Book(c.Start, c.End),
                (SlotState.Booked, Command.Cancel) => Cancel(),
                _ => throw new NotImplementedException()
            };
        public static IEnumerable<Event> Book(DateTime Start, DateTime End)
        {
            var ev = new Event.Booked(Guid.NewGuid(), Start, End, DateTime.UtcNow);
            events.Enqueue(ev);
            return events;
        }

        public static IEnumerable<Event> Cancel()
        {
            var ev = new Event.Canceled(Guid.NewGuid(), DateTime.UtcNow);
            events.Enqueue(ev);
            return events;
        }

    }
  
}
