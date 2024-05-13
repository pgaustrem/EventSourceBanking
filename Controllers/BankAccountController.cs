using System.IO.IsolatedStorage;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace EventSource.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankAccountController : ControllerBase
    {
        private readonly IStorage _storage;
        private readonly ILogger<BankAccountController> _logger;

        public BankAccountController(ILogger<BankAccountController> logger, IStorage storage)
        {
            _logger = logger;
            _storage = storage;
        }

        [HttpGet]
        public async Task<string> Get(Guid accountId)
        {
            return await _storage.Get(accountId);
        }

        [HttpPost]
        public async Task<Guid> Save()
        {
            var guid = Guid.NewGuid();
            await _storage.Save(guid);
            return guid;

        }


        //[HttpGet(Name = "GetBalance")]
        //public async Task<BankAccount> GetAccountDetails()
        //{
        //    return await _storage.Get() ?? throw new Exception("Account not found");
        //}

        //[HttpPost(Name = "CreateAccount")]
        //public async Task<CreatedResult> CreateAccount(string accountOwner)
        //{
        //    var account = new BankAccount
        //    {
        //        Balance = 0,
        //        LastLogin = DateTime.Now.AddDays(-3),
        //        AccountOwner = accountOwner                  
        //    };

        //    await _storage.Save(account);
        //    return Created(string.Empty, account.Id);
        //}

        //[HttpPost("{accountId}/withdraw", Name = "Withdraw")]
        //public async Task<BankAccount> WithdrawAsync(Guid accountId, int amount)
        //{
        //    var account = await _storage.Get(accountId) ?? throw new Exception("Account not found");
        //    account.Balance -= amount;
        //    await _storage.Save(account);
        //    return account;
        //}

        //[HttpPost("{accountId}/credit", Name = "Credit")]
        //public async Task<BankAccount> CreditAsync(Guid accountId, int amount)
        //{
        //    var account = await _storage.Get(accountId) ?? throw new Exception("Account not found");
        //    account.Balance += amount;
        //    await _storage.Save(account);
        //    return account;
        //}
    }
}
