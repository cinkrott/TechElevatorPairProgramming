using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class UserInterface
    {
        // This class provides all user communications, but not much else.
        // All the "work" of the application should be done elsewhere

        // ALL instances of Console.ReadLine and Console.WriteLine should 
        // be in this class.
        // NO instances of Console.ReadLine or Console.WriteLIne should be
        // in any other class.

        private Catering catering = new Catering();
        // private FileAccess fileAccess = new FileAccess();

        //todo Move GetInventory to Catering
        //public UserInterface()
        //{
        //    inventory = fileAccess.GetInventory();
        //}

        public void RunInterface()
        {
            bool done = false;
            while (!done)
            {
                try
                {
                    DisplayMenu();
                    string userResponse = Console.ReadLine();
                    switch (userResponse)
                    {
                        case "1":
                            PrintInventory();
                            Console.WriteLine();
                            break;
                        case "2":
                            RunOrderMenu();
                            break;
                        case "3":
                            done = true;
                            break;
                        default:
                            Console.WriteLine();
                            Console.WriteLine("Please enter a valid choice.");
                            break;
                    }

                }
                catch (FormatException e)
                {
                    Console.WriteLine("Format not valid.");
                    Console.WriteLine(e.Message);
                }

            }

        }
        public void DisplayMenu()
        {
            Console.WriteLine("Please make a selection:");
            Console.WriteLine("(1) Display Catering Items");
            Console.WriteLine("(2) Order");
            Console.WriteLine("(3) Quit");


        }
        public void OrderMenu()
        {
            Console.WriteLine("Please make a selection:");
            Console.WriteLine("(1) Add Money");
            Console.WriteLine("(2) Select Products");
            Console.WriteLine("(3) Complete Transaction");
            Console.WriteLine($"Current Account Balance: {catering.Balance}");


        }
        public void RunOrderMenu()
        {
            bool done = false;
            while (!done)
            {
                try
                {
                    OrderMenu();
                    string userResponse = Console.ReadLine();
                    switch (userResponse)
                    {
                        case "1":
                            GetMoney();
                            break;
                        case "2":
                            ProductSelection();
                            break;
                        case "3":
                            Receipt();
                            catering.GiveChange(catering.Balance);
                            done = true;
                            break;
                        default:
                            Console.WriteLine();
                            Console.WriteLine("Please enter a valid choice.");
                            break;
                    }

                }
                catch (FormatException e)
                {
                    Console.WriteLine("Format not valid.");
                    Console.WriteLine(e.Message);
                }

            }

        }

        public void PrintInventory()
        {
            List<CateringItem> inventory = catering.GetItems();
            foreach (CateringItem item in inventory)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void GetMoney()
        {
            try
            {
                Console.WriteLine("How much money would you like to add? Please enter whole dollar amount. ");
                decimal money = decimal.Parse(Console.ReadLine());


                catering.AddMoney(money);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Please enter amount as an integer.");
                Console.WriteLine(e.Message);
            }

        }

        public void ProductSelection()
        {
            try
            {
                Console.Write("Please enter item code: ");
                string itemCode = Console.ReadLine();
                Console.Write("Please enter the quantity: ");
                int quantity = int.Parse(Console.ReadLine());
                string purchaseSuccess = catering.AdjustInventory(itemCode, quantity);
                Console.WriteLine(purchaseSuccess);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Please enter quantity as an integer.");
                Console.WriteLine(e.Message);
            }

        }

        public void Receipt()
        {
            List<CateringItem> purchased = catering.CompleteTransaction();
            decimal totalSpent = 0;
            //decimal change = 0;


            foreach (CateringItem item in purchased)
            {
                Console.WriteLine($"{item.AmountBought}  {item.Type} {item.Name} {item.Price} {item.AmountBought * item.Price}");
                Console.WriteLine();
                totalSpent += (item.AmountBought * item.Price);
            }

            Console.WriteLine();
            Console.WriteLine($"Total: {totalSpent}");
            Console.WriteLine();

            if (catering.Balance > 0)
            {
                Dictionary<string, int> changeMade = catering.MakeChange(catering.Balance);

                Console.WriteLine("Your change is: ");
                foreach (KeyValuePair<string, int> kvp in changeMade)
                {
                    if (kvp.Value > 0)
                    {
                        Console.Write($"{kvp.Key.ToString()} : {kvp.Value.ToString()};  ");

                    }
                }

            }

            Console.WriteLine();
            Console.WriteLine();
        }



    }
}
