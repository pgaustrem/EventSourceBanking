namespace EventSource
{
    public class BankAccount
    {
        public DateTime LastLogin { get; set; } = DateTime.Now;

        public int Balance { get; set; }

        public int SpendingLimit => Random.Shared.Next(300, 3000);

        public string AccountOwner { get; set; } = string.Empty;
    }
}
