using System.Runtime.InteropServices.Marshalling;
using EventSource.Events;
using Microsoft.AspNetCore.Mvc;
using NEventStore.Domain.Core;

namespace EventSource
{
    public class BankAccount : AggregateBase
    {        
        private DateTime LastLogin { get; set; } = DateTime.Now;

        private int Balance { get; set; }

        private int SpendingLimit => Random.Shared.Next(300, 3000);

        private string AccountOwner { get; set; } = string.Empty;

        public BankAccount(Guid id) : base(new ConventionEventRouter(throwOnApplyNotFound: true))
        {
            Id = id;
            RaiseEvent(new AccountCreated { Id = id });
        }


        public void Credit(int amount)
        {
            if (amount < 0)
                throw new Exception();

            RaiseEvent(new AccountCredited { Amount = amount });
        }

        public void Apply(AccountCredited @event) 
        {
            Balance += @event.Amount;
        }        

        public void Apply(AccountCreated @event) 
        { 
            Id = @event.Id;
            Balance = 0;
        }
    }


}
