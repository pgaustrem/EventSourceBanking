using System.IO.IsolatedStorage;
using System.Text.Json;
using EventSource.Commands;
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
        public async Task<BankAccount> Get(Guid accountId)
        {
            return await _storage.Get(accountId);
        }

        [HttpPost]
        public async Task<Guid> CreateAccount()
        {
            var accountId = Guid.NewGuid();
            await _storage.Handle(new CreateAccountCommand(accountId));
            return accountId;
        }

        [HttpPost("{accountId}/credit", Name = "Credit")]
        public async Task<BankAccount> CreditAsync(Guid accountId, int amount)
        {
           await  _storage.Handle(new CreditAccountCommand { Id = accountId, Amount = amount });
           return await Get(accountId);
        }        
    }
}
