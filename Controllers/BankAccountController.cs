using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace EventSource.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankAccountController : ControllerBase
    {

        private readonly ILogger<BankAccountController> _logger;

        public BankAccountController(ILogger<BankAccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetBalance")]
        public async Task<BankAccount> GetAccountDetails()
        {
            return await Storage.Get() ?? throw new Exception("Account not found");
        }

        [HttpPost(Name = "CreateAccount")]
        public async Task<CreatedResult> CreateAccount(string accountOwner)
        {
            var account = new BankAccount
            {
                Balance = 0,
                LastLogin = DateTime.Now.AddDays(-3),
                AccountOwner = accountOwner
            };

            await Storage.Save(account);
            return Created();
        }

        [HttpPost("/withdraw", Name = "Withdraw")]
        public async Task<BankAccount> WithdrawAsync(int amount)
        {
            var account = await Storage.Get() ?? throw new Exception("Account not found");
            account.Balance -= amount;
            await Storage.Save(account);
            return account;
        }

        [HttpPost("/credit", Name = "Credit")]
        public async Task<BankAccount> CreditAsync(int amount)
        {
            var account = await Storage.Get() ?? throw new Exception("Account not found");
            account.Balance += amount;
            await Storage.Save(account);
            return account;
        }
    }
}
