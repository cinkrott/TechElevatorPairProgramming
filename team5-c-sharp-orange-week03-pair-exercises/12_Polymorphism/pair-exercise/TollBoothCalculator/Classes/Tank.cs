using System;
using System.Collections.Generic;
using System.Text;

namespace TollBoothCalculator.Classes
{
    class Tank : IVehicle
    {
        //method
        public double CalculateToll(int distance)
        {
            double toll = distance * 0;
            return toll;
        }
        public string GetName()
        {
            return "Tank              ";
        }
        public int GetRandomMiles()
        {
            Random rand = new Random();
            int randomMiles = rand.Next(10, 241);
            return randomMiles;
        }
    }
}
