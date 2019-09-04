using System.Collections.Generic;

namespace Support_Bank
{
    public class Banking
    {
        public List<Account> accounts = new List<Account>();


        public Account FindOrCreateAccount(string accountName)
        {

            bool CheckAccountName(Account account)
            {
                return account.Name == accountName;
            }

            var account = accounts.Find(CheckAccountName);
            if(account != null)
            {
                return account;
            }
            else
            {
                account = new Account();
                account.Name = accountName;
                account.Balance = 0;
                accounts.Add(account);
                return account;
            }

        }

    }
}