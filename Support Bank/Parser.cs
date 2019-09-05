using NLog;
using System;
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
            try
            {
                logger.Info("Parsing started");
                var transactions = new List<Transaction>();
                if (!File.Exists(path))
                {
                    logger.Error($"File {path} not found");
                    Console.WriteLine("File not found, please check the pathway");
                    return null;
                }
                //because header is skipped.
                var lineCounter = 1;
                foreach (string s in File.ReadAllLines(path).Skip(1))
                {
                    lineCounter++;
                    var transaction = new Transaction();
                    string[] data = s.Split(',');
                    transaction.Date = data[0];
                    transaction.Sender = data[1];
                    transaction.Reciever = data[2];
                    transaction.Reason = data[3];
                    try
                    {
                        transaction.Amount = decimal.Parse(data[4]);
                    }
                    catch(FormatException e)
                    {
                        //TODO: throw error for this line, then continue on with the rest of the transactions.
                        logger.Error("The input in the amount column on row {0} is wrong, '{1}' is not a decimal.", lineCounter, data[4]);
                        throw;
                    }

                    transactions.Add(transaction);
                }
                logger.Info("Parsing finished");
                return transactions;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                logger.Error(e.Message);
                throw;
            }
        }
    }
}