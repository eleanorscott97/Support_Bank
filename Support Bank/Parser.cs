using NLog;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Support_Bank
{
    public class Parser
    {
        private readonly Logger logger;

        public Parser()
        {
            logger = LogManager.GetLogger("Parser");
        }

        public List<Transaction> ParseCsvFile(string path)
        {
            logger.Info("Parsing started");
            var transactions = new List<Transaction>();
            if (!File.Exists(path))
            {
                return null;
            }
            foreach (string s in File.ReadAllLines(path).Skip(1))
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
            logger.Info("Parsing finished");
            return transactions;
        }
    }
}