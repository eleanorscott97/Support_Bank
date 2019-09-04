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

            foreach(var transaction in transactions)
            {
                //find sender account
                var accountS = banking.FindOrCreateAccount(transaction.Sender);
                var accountR = banking.FindOrCreateAccount(transaction.Reciever);
                //change balances
                accountS.Balance += -transaction.Amount;
                accountR.Balance += transaction.Amount;
            }

            var input = GetUserInput();
            if(input.Type == CommandType.ListAll)
            {
                foreach(var account in banking.accounts)
                {
                    Console.WriteLine("Name:" + account.Name + ", Account Balance:" + account.Balance);
                }
            }
            else if(input.Type == CommandType.ListSingle)
            {
                foreach(Transaction transaction in transactions)
                {
                    if((transaction.Sender == input.Target || (transaction.Reciever == input.Target)))
                    {
                        Console.WriteLine("Date:" + transaction.Date + ", Reason:" + transaction.Reason +", Amount:" + transaction.Amount);
                    }
                }
            }
            else
            {
                string[] Name = Console.ReadLine().Split('[',']');
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

            if(input.StartsWith("List "))
            {
                return new UserInput
                {
                    Type = CommandType.ListSingle,
                    Target = input.Substring(5)
                };
            }

            return null;
        }
    }

    public class UserInput
    {
        public CommandType Type { get; set; }
        public string Target { get; set; }
    }

    public enum CommandType
    {
        ListAll,
        ListSingle
    }
}
