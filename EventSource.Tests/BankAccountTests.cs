using EventSource.Events;
using NEventStore.Domain;
using Xunit;

namespace EventSource.Tests
{
    public class BankAccountTests
    {
        [Fact]
        public void CreateBankAccount_RaisesAccountCreatedEvent()
        {
            var accountId = Guid.NewGuid();

            var account = new BankAccount(accountId);

            var uncommittedEvents = ((IAggregate)account).GetUncommittedEvents().Cast<object>().ToList();
            Assert.Single(uncommittedEvents);
            var createdEvent = Assert.IsType<AccountCreated>(uncommittedEvents[0]);
            Assert.Equal(accountId, createdEvent.Id);
        }

        [Fact]
        public void CreateBankAccount_SetsId()
        {
            var accountId = Guid.NewGuid();

            var account = new BankAccount(accountId);

            Assert.Equal(accountId, account.Id);
        }

        [Fact]
        public void CreditBankAccount_RaisesAccountCreditedEvent()
        {
            var account = new BankAccount(Guid.NewGuid());
            ((IAggregate)account).ClearUncommittedEvents();

            account.Credit(100);

            var uncommittedEvents = ((IAggregate)account).GetUncommittedEvents().Cast<object>().ToList();
            Assert.Single(uncommittedEvents);
            var creditedEvent = Assert.IsType<AccountCredited>(uncommittedEvents[0]);
            Assert.Equal(100, creditedEvent.Amount);
        }

        [Fact]
        public void CreditBankAccount_WithNegativeAmount_ThrowsException()
        {
            var account = new BankAccount(Guid.NewGuid());

            Assert.Throws<Exception>(() => account.Credit(-1));
        }
    }
}
