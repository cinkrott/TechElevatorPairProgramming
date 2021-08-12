using System;

namespace MakeChange
{
    class Program
    {
        /*
        Write a command line program which prompts the user for the total bill, and the amount tendered. It should then display the change required.

        C:\Users> MakeChange

        Please enter the amount of the bill: 23.65
        Please enter the amount tendered: 100.00
        The change required is 76.35
        */
        static void Main(string[] args)
        {

            

            Console.WriteLine("Please enter the amount of bill: ");
            string userInput = Console.ReadLine();
            decimal bill = decimal.Parse(userInput);

            Console.WriteLine("Please enter the amount tendered: ");
            userInput = Console.ReadLine();
            decimal pay = decimal.Parse(userInput);

            decimal change = (pay - bill);

            Console.WriteLine("The change required is: " + change);

        }

        
    }
}
