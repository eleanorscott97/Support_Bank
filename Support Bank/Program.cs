using System;
using System.Collections.Generic;
using System.IO;

namespace Support_Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            var transactions = new List<Transaction>();
            var accounts = new List<Account>();
            // open cvs file
            string path = @"C:\Work\Training\Support Bank\Transactions2014.csv";

            foreach(string s in File.ReadAllLines(path))
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
            foreach(var r in transactions)
            {
                accounts.Find(a => a.Name == r.Reciever);
                //is there already an account name
                if()
                //check who is paying money (what the debt is)

            }
        }
    }
}
