using System;

namespace ADO_AddressBook
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Address Book!");
            TransactionClass transactionClass = new TransactionClass();
            transactionClass.AddMultipleDataToList();
        }
    }
}
