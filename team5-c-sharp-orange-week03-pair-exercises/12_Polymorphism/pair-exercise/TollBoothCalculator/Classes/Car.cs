using System;
using System.Collections.Generic;
using System.Text;

namespace TollBoothCalculator.Classes
{
    class Car : IVehicle
    {
        //properties
        public bool HasTrailer { get; }

        //constructors
        public Car(bool hasTrailer)
        {
            HasTrailer = hasTrailer;
        }

        //method
        public double CalculateToll(int distance)
        {
            double toll = distance * 0.020;
            if (HasTrailer)
            {
                toll += 1.00;
            }
            else
            {

            }
            return toll;
        }
        public string GetName()
        {
            if (HasTrailer)
            {
                return "Car (with trailer)";
            }
            else
            {
            return "Car               ";
            }
        }
        public int GetRandomMiles()
        {
            Random rand = new Random();
            int randomMiles = rand.Next(10, 241);
            return randomMiles;
        }
    }
}
