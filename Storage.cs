using System.Text;
using System.Text.Json;
using EventSource.Events;
using Microsoft.Identity.Client.Extensions.Msal;
using NEventStore;

namespace EventSource
{
    public interface IStorage
    {
        Task<string> Get(Guid accountId);
        Task Save(Guid accountId);
    }

    public class Storage(IStoreEvents events) : IStorage
    {
        private readonly IStoreEvents _eventStore = events;

        public async Task Save(Guid accountId)
        {
            using (var stream = _eventStore.CreateStream(accountId))
            {
                var someEvent = new SomethingHappened() { Seed = Random.Shared.Next() };                
                stream.Add(new EventMessage { Body = someEvent});
                stream.CommitChanges(Guid.NewGuid());
            }
        }

        public async Task<string> Get(Guid accountId)
        {
            var returnValues = new StringBuilder();
            using(var stream = _eventStore.OpenStream(accountId))
            {
                foreach (var @event in stream.CommittedEvents) 
                {
                    returnValues.AppendLine(@event.Body.ToString());
                } 
            }

            return returnValues.ToString();
        }
    }
}
