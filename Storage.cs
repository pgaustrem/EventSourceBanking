using System.Text;
using System.Text.Json;
using EventSource.Commands;
using EventSource.Events;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Extensions.Msal;
using NEventStore;
using NEventStore.Domain.Persistence;

namespace EventSource
{
    public interface IStorage
    {
        Task<BankAccount> Get(Guid accountId);
        Task Handle(CreditAccountCommand command);
        Task Handle(CreateAccountCommand command);
    }

    public class Storage(IRepository repository) : IStorage
    {
        private readonly IRepository _repository = repository;

        public async Task Handle(CreateAccountCommand command)
        {
            var aggregateRoot = new BankAccount(command.Id);
            
            _repository.Save(aggregateRoot, Guid.NewGuid());
        }

        public async Task Handle(CreditAccountCommand command)
        {
            var aggregateRoot = _repository.GetById<BankAccount>(command.Id);
            aggregateRoot.Credit(100);
            _repository.Save(aggregateRoot, Guid.NewGuid());
        }

        public async Task<BankAccount> Get(Guid accountId)
        {            
            var aggregateRoot = _repository.GetById<BankAccount>(accountId);

            return aggregateRoot;
        }
    }
}
