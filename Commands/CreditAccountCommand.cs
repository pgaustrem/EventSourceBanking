namespace EventSource.Commands
{
    public abstract class BaseCommand
    {
        public Guid Id { get; set; }
    }
    public class CreditAccountCommand : BaseCommand
    {
        public int Amount { get; set; }
    }

    public class CreateAccountCommand : BaseCommand
    {
        public string AccountOwnerName { get; set; } = string.Empty;

        public CreateAccountCommand(Guid accountId) 
        {
            Id = accountId;
        }
    }
}
