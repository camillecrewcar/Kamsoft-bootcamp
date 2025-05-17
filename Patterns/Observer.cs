namespace Szkolenie_Kamsoft.Patterns
{
    public abstract class Publisher 
    {
        public event EventHandler Handler;

        public void Publish(EventArgs args) 
        {
            Handler?.Invoke(this, args);
        }
    }

    public abstract class Subscriber
    {
        public abstract void OnEventRaised(object sender, EventArgs args);
        public void Subscribe(Publisher publisher)
        {
            publisher.Handler += OnEventRaised;
        }
        public abstract void Unsubscribe(Publisher publisher);
    }
}
