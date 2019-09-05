using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.IO;

namespace Support_Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var config = new LoggingConfiguration();
                var target = new FileTarget { FileName = @"C:\Work\Logs\SupportBank.log", Layout = @"${longdate} ${level} - ${logger}: ${message}" };
                config.AddTarget("File Logger", target);
                config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, target));
                LogManager.Configuration = config;

                var parser = new Parser();
                var banking = new Banking();

                string path = @"C:\Work\Training\Support_Bank\DodgyTransactions2015.csv";
                var transactions = parser.ParseCsvFile(path);
                banking.UpdateBalances(transactions);
                while (true)
                {
                    var input = GetUserInput();
                    if (input.Type == CommandType.ListAll)
                    {
                        DisplayNameAndBalance(banking);
                    }
                    else if (input.Type == CommandType.ListSingle)
                    {
                        DisplayDateReasonAndAmount(transactions, input);
                    }
                    else
                    {
                        Console.WriteLine("Input was invalid");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("The program encountered a fatal error and needs to close");
                Console.WriteLine(e.Message);
                var logger = LogManager.GetLogger("SupportBank");
                logger.Fatal(e.Message);
            }
        }

        private static UserInput GetUserInput()
        {
            var logger = LogManager.GetLogger("UserInput");
            logger.Info("Getting user input"); 
            Console.WriteLine("What information would you like? (List All; List [User]) ");
            var input = Console.ReadLine();
            if (input == "List All")
            {
                return new UserInput
                {
                    Type = CommandType.ListAll
                };
            }
            if (input.StartsWith("List "))
            {
                return new UserInput
                {
                    Type = CommandType.ListSingle,
                    Target = input.Substring(5)
                };
            }
            logger.Error("Invalid user input given");
            return null;
        }
        private static void DisplayNameAndBalance(Banking banking)
        {
            foreach (var account in banking.accounts)
            {
                Console.WriteLine("Name:" + account.Name + ", Account Balance:" + account.Balance);
            }
        }
        private static void DisplayDateReasonAndAmount(List<Transaction> transactions, UserInput input)
        {
            foreach (Transaction transaction in transactions)
            {
                if ((transaction.Sender == input.Target || (transaction.Reciever == input.Target)))
                {
                    Console.WriteLine("Date:" + transaction.Date + ", Reason:" + transaction.Reason + ", Amount:" + transaction.Amount);
                }
            }
        }
    }
}
