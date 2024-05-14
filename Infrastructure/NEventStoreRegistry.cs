using System.Reflection;
using NEventStore;
using NEventStore.Domain.Persistence;
using NEventStore.Domain;
using NEventStore.Serialization.Json;
using NEventStore.Persistence.Sql.SqlDialects;

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
              .UsingSqlPersistence(Microsoft.Data.SqlClient.SqlClientFactory.Instance, @"Data Source=.\sqlexpress; Initial Catalog=NEventStore; Integrated Security=True; TrustServerCertificate=true")
              .WithDialect(new MsSqlDialect())
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
