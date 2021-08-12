using System;
using System.Collections.Generic;
using System.Text;

namespace TollBoothCalculator.Classes
{
    class Truck : IVehicle
    {
        public int NumberOfAxles { get; }

            //constructor
        public Truck(int numberOfAxles)
        {
            NumberOfAxles = numberOfAxles;
        }

        //method
        public double CalculateToll(int distance)
        {
            double ratePerMile = 0;
            double toll = distance;

            if (NumberOfAxles == 4)
            {
                ratePerMile = 0.040;
            }
            else if (NumberOfAxles == 6)
            {
                ratePerMile = 0.045;
            }
            else if (NumberOfAxles >= 8)
            {
                ratePerMile = 0.048;
            }
            toll *= ratePerMile;
            return toll;
        }
        public string GetName()
        {
            string name = "";
            if (NumberOfAxles == 4)
            {
                name = "Truck (4 axels)   ";
            }
            else if (NumberOfAxles == 6)
            {
                name = "Truck (6 axels)   ";
            }
            else
            {
                name = "Truck (8 axels)   ";
            }
            return name;
        }
        public int GetRandomMiles()
        {
            Random rand = new Random();
            int randomMiles = rand.Next(10, 241);
            return randomMiles;
        }
    }
}
