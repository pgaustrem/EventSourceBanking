using NEventStore;

namespace EventSource.Events
{

    public class SomethingHappened 
    {
        public SomethingHappened()
        {
        }

        public int Seed { get; set; }
    }
}
