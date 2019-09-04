using System.Collections.Generic;

namespace Support_Bank
{
    public class Banking
    {
        public List<Account> accounts = new List<Account>();


        public Account FindOrCreateAccount(string accountName)
        {

            bool CheckAccountName(Account a)
            {
                return a.Name == accountName;
            }

            var account = accounts.Find(CheckAccountName);
            if(account != null)
            {
                return account;
            }
            else
            {
                account = new Account
                {
                    Name = accountName,
                    Balance = 0
                };
                accounts.Add(account);
                return account;
            }

        }

    }
}