using MiniBank.Repository;
using MiniBank.Models;
using System.Xml.Linq;

namespace MiniBank.Tests
{
    public class Account_Json_Repository_Should
    {

        private readonly string _testFilePath = @"C:\Users\marii\OneDrive\Desktop\BCTSO-20-NC-2-main\BCTSO-20-NC-2\MiniBank.Tests\Data\Accounts.json";
        [Fact]
        public void get_All_Acounts()
        {
            var dataTest = new AccountJsonRepository(_testFilePath);
            
            // Act
            var result = dataTest.GetAccounts();

            // Assert
            Assert.Equal(6,result.Count);
        }

        [Fact]
        public void Get_Accounts_OfCustomer()
        {
            var dataTest = new AccountJsonRepository(_testFilePath);
            var customerId = 1;
            var result = dataTest.GetAccountsOfCustomer(customerId);

            Assert.Equal(3, result.Count);

        }

        [Fact]
        public void Add_new_Account()
        {
            var dataTest = new AccountJsonRepository(_testFilePath);
            var newAccount = new Account()
            {
                Id = 7,
                Iban = "GE94SB5621487456325158",
                Currency = "USD",
                Balance = 23,
                CustomerId = 1,
                Name = "testAcount"
            };
            dataTest.Create(newAccount);
            var actual= dataTest.GetAccounts().Count;
            Assert.Equal(7, actual);

        }
        [Fact]
        public void DeleteId()
        {
            var dataTest = new AccountJsonRepository(_testFilePath);
            var  accountIdToDelete = 3;
            var expected = 6;

            dataTest.Delete(accountIdToDelete);

            var actual= dataTest.GetAccounts().Count;
            Assert.Equal(expected, actual);
        }

    }
}
