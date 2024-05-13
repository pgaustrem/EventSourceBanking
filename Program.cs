using EventSource;
using EventSource.Infrastructure;
using NEventStore;
using NEventStore.Domain.Persistence.EventStore;
using NEventStore.Domain.Persistence;
using NEventStore.Domain.Core;
using NEventStore.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(typeof(IStoreEvents), NEventStoreRegistry.Setup());
builder.Services.AddScoped<IConstructAggregates, AggregateFactory>();
builder.Services.AddScoped<IDetectConflicts, ConflictDetector>();
builder.Services.AddScoped<IRepository, EventStoreRepository>();
builder.Services.AddScoped<IStorage, Storage>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
