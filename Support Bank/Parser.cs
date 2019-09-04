using System.Collections.Generic;
using System.IO;

namespace Support_Bank
{
    public class Parser
    {
        public List<Transaction> ParseCsvFile(string path)
        {
            var transactions = new List<Transaction>();
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
            return transactions;
        }
    }
}