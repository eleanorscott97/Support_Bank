using NLog;
using System;
using System.Collections.Generic;

namespace Support_Bank
{
    public class Banking
    {
        public List<Account> accounts = new List<Account>();
        private readonly Logger logger;
        public Banking()
        {
            logger = LogManager.GetLogger("Banking");
        }
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
        public void UpdateBalances(List<Transaction> transactions)
        {
            try
            {
                logger.Info("Updating balances");
                foreach (var transaction in transactions)
                {
                    var SenderAccount = FindOrCreateAccount(transaction.Sender);
                    var RecieverAccount = FindOrCreateAccount(transaction.Reciever);
                    SenderAccount.Balance -= transaction.Amount;
                    RecieverAccount.Balance += transaction.Amount;
                }
                logger.Info("Balances updated");
            }
            catch(Exception e)
            {
                logger.Error("Couldn't update balances");
                Console.WriteLine(e.Message);

            }
        }

    }
}