using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    public class FileAccess
    {
        // all files for this application should in this directory
        // you will likley need to create it on your computer
        private string filePath = @"C:\Catering\cateringsystem.csv";

        // This class should contain any and all details of access to files

        public List<CateringItem> GetInventory()
        {
            List<CateringItem> cateringItems = new List<CateringItem>();

           

                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] split = line.Split('|');

                        CateringItem item = new CateringItem();

                        item.Code = split[0];
                        item.Name = split[1];
                        item.Price = decimal.Parse(split[2]);
                        item.Type = split[3];
                        if (split[3].StartsWith('B'))
                        {
                            item.Type = "Beverage";
                        }
                        else if (split[3].StartsWith('A'))
                        {
                            item.Type = "Appetizer";
                        }
                        if (split[3].StartsWith('E'))
                        {
                            item.Type = "Entree";
                        }
                        if (split[3].StartsWith('D'))
                        {
                            item.Type = "Dessert";
                        }

                        item.Quantity = 50;

                        item.AmountBought = 0;

                        cateringItems.Add(item);
                    }
                }
            

            return cateringItems;
        }
        public bool PrintLog(decimal money, decimal balance)
        {
            bool logPrint = false;
           
                using (StreamWriter sw = new StreamWriter(@"C:\Catering\log.txt", true))
                {
                    sw.WriteLine($"{DateTime.Now} ADD MONEY {money.ToString("C")} {balance.ToString("C")}");
                }
           
            return logPrint;
        }


        public bool PrintLog(int amountBought, string name, string code, decimal cost, decimal balance)
        {
            bool logPrint = false;
            
                using (StreamWriter sw = new StreamWriter(@"C:\Catering\log.txt", true))
                {
                    sw.WriteLine($"{DateTime.Now} {amountBought} {name} {code} {(amountBought * cost).ToString("C")} {balance.ToString("C")}");
                }
            
            
            return logPrint;

        }
        public bool PrintLog(decimal money)
        {
            bool logPrint = false;
            
                using (StreamWriter sw = new StreamWriter(@"C:\Catering\log.txt", true))
                {
                    sw.WriteLine($"{DateTime.Now} GIVE CHANGE {money.ToString("C")} $0.00");
                }
            
            return logPrint;
        }





    }
}

