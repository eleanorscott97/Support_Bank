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

            string path = @"C:\Work\Training\Support Bank\Transactions2014.csv";
            var transactions = parser.ParseCsvFile(path);
            UpdateBalances(transactions, banking);
            while (true)
            {
                var input = GetUserInput();
                if (input.Type == CommandType.ListAll)
                {
                    NameAndBalance(banking);
                }
                else if (input.Type == CommandType.ListSingle)
                {
                    DateReasonAndAmount(transactions, input);
                }
                else
                {
                    Console.WriteLine("Input was invalid");
                }
            }
        }
        private static UserInput GetUserInput()
        {
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
            return null;
        }
        private static void UpdateBalances( List<Transaction> transactions, Banking banking)
        {
            foreach (var transaction in transactions)
            {
                var SenderAccount = banking.FindOrCreateAccount(transaction.Sender);
                var RecieverAccount = banking.FindOrCreateAccount(transaction.Reciever);
                SenderAccount.Balance -= transaction.Amount;
                RecieverAccount.Balance += transaction.Amount;
            }
        }
        private static void NameAndBalance(Banking banking)
        {
            foreach (var account in banking.accounts)
            {
                Console.WriteLine("Name:" + account.Name + ", Account Balance:" + account.Balance);
            }
        }
        private static void DateReasonAndAmount(List<Transaction> transactions, UserInput input)
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
