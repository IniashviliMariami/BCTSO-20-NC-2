using MiniBank.Models;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MiniBank.Repository
{
    public class AccountJsonRepository
    {
        private readonly string _filePath;
        private List<Account> _accounts;

        public AccountJsonRepository(string filePath)
        {
            _filePath = filePath;
            _accounts = LoadData();
        }

        public List<Account> GetAccounts() => _accounts;

        public List<Account> GetAccountsOfCustomer(int customerId) => _accounts.Where(x => x.CustomerId == customerId).ToList();

        public Account GetAccount(int id) => _accounts.FirstOrDefault(x => x.Id == id);

        public void Create(Account account)
        {
            _accounts.Add(account);
            SaveData();
        }

        public void Update(Account account)
        {
            var changeId = _accounts.FirstOrDefault(a => a.Id == account.Id);
            if (changeId.Id >= 0)
            {
                _accounts[changeId] = account;
                SaveData();
            }
        }

        public void Delete(int id)
        {
           var deleteId=_accounts.FirstOrDefault(a=>a.Id== id);
            if(deleteId.Id != null)
            {
                _accounts.Remove(deleteId);
                SaveData();
            }

        }

        private void SaveData()
        {

            string jsonData=JsonSerializer.Serialize(_accounts, new JsonSerializerOptions());
            File.WriteAllText(_filePath,jsonData);
           
        }

        private List<Account> LoadData()
        {
            if (!File.Exists(_filePath))
            {
               return new List<Account>();
            }
           string text = File.ReadAllText(_filePath);
           var dataList = JsonSerializer.Deserialize<List<Account>>(text);
            return dataList;
        }
    }
}
