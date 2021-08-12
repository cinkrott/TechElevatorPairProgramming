using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Catering
    {
        // This class should contain all the "work" for catering

        private List<CateringItem> items = new List<CateringItem>();
        FileAccess file = new FileAccess();

        public List<CateringItem> itemsPurchased = new List<CateringItem>();


        public decimal Balance { get; set; }



        public Catering()
        {
            Balance = 0;
            items = file.GetInventory();
        }

        public List<CateringItem> GetItems()
        {
            return items;
        }

        public decimal AddMoney(decimal moneyAdded)
        {
            if (Balance + moneyAdded > 5000M)
            {
                return Balance;
            }
            else
            {
                Balance += moneyAdded;
                file.PrintLog(moneyAdded, Balance);
            }
            return Balance;
        }

        public string AdjustInventory(string code, int quantity)
        {
            string purchaseSuccess = "";
            foreach (CateringItem item in items)
            {
                if (item.Code == code)
                {
                    if (quantity <= item.Quantity && Balance >= item.Price * quantity)
                    {
                        item.Quantity -= quantity;
                        item.AmountBought = quantity;
                        Balance -= item.Price * quantity;
                        purchaseSuccess = "Purchase successful.";
                        file.PrintLog(item.AmountBought, item.Name, item.Code, item.Price, Balance);
                        break;
                    }
                    else if (item.Quantity == 0)
                    {
                        purchaseSuccess = "That item is sold out.";
                        break;
                    }
                    else if (Balance < item.Price * quantity)
                    {
                        purchaseSuccess = "Insufficient funds.";
                        break;
                    }
                    else if (item.Quantity < quantity)
                    {
                        purchaseSuccess = "Insufficient stock.";
                        break;
                    }

                }
                else if (item.Code != code)
                {
                    purchaseSuccess = "Item does not exist.";
                }

            }
            return purchaseSuccess;
        }

        public List<CateringItem> CompleteTransaction()
        {
            List<CateringItem> purchased = new List<CateringItem>();
            foreach (CateringItem item in items)
            {
                if (item.Quantity < 50)
                {
                    purchased.Add(item);

                }
            }
            return purchased;

        }

        public decimal GiveChange(decimal balance)
        {
            decimal change = Balance;
            Balance = 0;
            file.PrintLog(change);
            return Balance;

        }


        public Dictionary<string, int> MakeChange(decimal balance)
        {
            Dictionary<string, int> change = new Dictionary<string, int>();
            string intBalance = balance.ToString();
            int dollars;
            if (intBalance.Contains('.'))

            {
            string[] splitBalance = intBalance.Split('.');
            dollars = int.Parse(splitBalance[0]);
            int cents = int.Parse(splitBalance[1]);
            change["20"] = dollars / 20;
            dollars -= change["20"] * 20;
            change["10"] = dollars / 10;
            dollars -= change["10"] * 10;
            change["5"] = dollars / 5;
            dollars -= change["5"] * 5;
            change["1"] = dollars / 1;

            change[".25"] = cents / 25;
            cents -= change[".25"] * 25;
            change[".10"] = cents / 10;
            cents -= change[".10"] * 10;
            change[".05"] = cents / 5;
            }
            else
            {
                dollars = int.Parse(intBalance);
                change["20"] = dollars / 20;
                dollars -= change["20"] * 20;
                change["10"] = dollars / 10;
                dollars -= change["10"] * 10;
                change["5"] = dollars / 5;
                dollars -= change["5"] * 5;
                change["1"] = dollars / 1;
            }

            


            

            return change;
        }


    }
}
