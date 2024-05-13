using NEventStore;
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
}
