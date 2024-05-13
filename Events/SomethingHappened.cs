using NEventStore;

namespace EventSource.Events
{

    public class AccountCreated 
    {
        public Guid Id { get; set; }
    }

    public class AccountCredited 
    {
        public int Amount { get; set; }
    }
}
