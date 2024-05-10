using System.Text.Json;

namespace EventSource
{
    public static class Storage
    {
        public async static Task Save(BankAccount weather) 
        {
            string fileName = "persist.json";
            await using FileStream createStream = File.OpenWrite(fileName);
            await JsonSerializer.SerializeAsync(createStream, weather);
        }

        public async static Task<BankAccount?> Get()
        {
            BankAccount? bankAccount;
            string fileName = "persist.json";
            using FileStream openStream = File.OpenRead(fileName);
            try
            {
                bankAccount = await JsonSerializer.DeserializeAsync<BankAccount>(openStream);
            }
            catch(Exception) { return null; }
            
            return bankAccount;
        }
    }
}
