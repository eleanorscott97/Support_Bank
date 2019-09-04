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
            var transactions = new List<Transaction>();
            var accounts = new List<Account>();
            // open cvs file
            string path = @"C:\Work\Training\Support Bank\Transactions2014.csv";

            var transactions = parser.ParseCsvFile(path);

            foreach (string s in File.ReadAllLines(path))
            {
                var transaction = new Transaction();
                string[] data = s.Split(',');
                transaction.Date = data[0];
                transaction.Sender = data[1];
                transaction.Reciever = data[2];
                transaction.Reason = data[3];
                transaction.Amount = decimal.Parse(data[4]);

                transactions.Add(transaction);
            }

            foreach(var transaction in transactions)
            {
                if(accounts.Find(a => a.Name == transaction.Sender) == null)
                {
                    var accountSender = new Account();
                    accountSender.Name = transaction.Sender;
                    accountSender.Balance = -transaction.Amount;
                    accounts.Add(accountSender);
                }
                else if(accounts.Find(a => a.Name == transaction.Reciever) == null)
                {
                    var accountReciever = new Account();
                    accountReciever.Name = transaction.Reciever;
                    accountReciever.Balance = 0;
                    accounts.Add(accountReciever);
                }
                else
                {

                }
                //check who is paying money (what the debt is)

            }
        }
    }
}
