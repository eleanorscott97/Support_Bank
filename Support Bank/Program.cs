using System;
using System.Collections.Generic;
using System.IO;

namespace Support_Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser();
            var banking = new Banking();
            var accounts = new List<Account>();

            string path = @"C:\Work\Training\Support Bank\Transactions2014.csv";
            var transactions = parser.ParseCsvFile(path);

            foreach(var transaction in transactions)
            {
                //find sender account
                var accountS = banking.FindOrCreateAccount(transaction.Sender);
                var accountR = banking.FindOrCreateAccount(transaction.Reciever);
                //change balances
                accountS.Balance += -transaction.Amount;
                accountR.Balance += transaction.Amount;
            }

        }
    }
}
