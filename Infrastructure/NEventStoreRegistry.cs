using System.Reflection;
using NEventStore;
using NEventStore.Domain.Persistence;
using NEventStore.Domain;
using NEventStore.Serialization.Json;

namespace EventSource.Infrastructure
{
    public class NEventStoreRegistry
    {
        public static IStoreEvents Setup()
        {
            var loggerFactory = LoggerFactory.Create(logging =>
            {
                logging
                    .SetMinimumLevel(LogLevel.Trace); ;
            });

            return Wireup.Init()
              .WithLoggerFactory(loggerFactory)
              .UsingInMemoryPersistence()
              .InitializeStorageEngine()
              .UsingJsonSerialization()                      
              .Build();
        }
    }

    public class AggregateFactory : IConstructAggregates
    {
        public IAggregate Build(Type type, Guid id, IMemento snapshot)
        {
            var constructor = type.GetConstructors()[0];
            //(BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(Guid) }, null);

            return constructor.Invoke(new object[] { id }) as IAggregate;
        }
    }
}
